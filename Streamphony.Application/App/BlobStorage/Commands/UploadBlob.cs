using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Configurations;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.BlobStorage.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.BlobStorage.Commands;

public record UploadBlob(BlobRequestDto BlobRequestDto, Guid UserId, string BlobType) : IRequest<BlobDto>;

public class UploadBlobHandler(
    IBlobStorageService blobStorageService,
    IUserManagerProvider userManagerProvider,
    IMappingProvider mapper,
    IFileStorageSettings fileStorageSettings,
    IUnitOfWork unitOfWork,
    IValidationService validationService,
    IAudioDurationService audioDurationService) : IRequestHandler<UploadBlob, BlobDto>
{
    private readonly IBlobStorageService _blobStorage = blobStorageService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IFileStorageSettings _fileStorageSettings = fileStorageSettings;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IAudioDurationService _audioDurationService = audioDurationService;

    public async Task<BlobDto> Handle(UploadBlob request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<BlobType>(request.BlobType, true, out var blobType))
            throw new InvalidOperationException("Invalid blob type.");
        
        var blobRequestDto = request.BlobRequestDto;
        var userId = request.UserId;
        var isAudio = blobType == BlobType.Song;
        var isProfilePicture = blobType == BlobType.ProfilePicture;
        var fileFolder = isAudio ? BlobContainer.Songs : BlobContainer.Images;
        
        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, userId, cancellationToken); 
        var ownerId = userDb.Id;
        if (!isProfilePicture)
        {
            var artistId = userDb.ArtistId;
            await _validationService.AssertNavigationEntityExists<User, Artist>(_unitOfWork.ArtistRepository, artistId, cancellationToken, isNavRequired: true);
            ownerId = artistId!.Value;
        }

        ValidateFileSize(blobRequestDto.Length, isAudio);
        var fileExtension = ValidateFileExtension(blobRequestDto.FileName, blobRequestDto.ContentType, isAudio);

        var newBlobId = Guid.NewGuid();
        var newBlobName = $"{fileFolder}/{newBlobId.ToString()}";
        var containerName = BlobContainer.Draft;
        
        var url = await _blobStorage.UploadFileAsync(
            containerName,
            newBlobName,
            blobRequestDto.ContentType,
            blobRequestDto.Content!
        );
        if (string.IsNullOrWhiteSpace(url)) throw new Exception("Failed to upload file");
        
        TimeSpan? audioDuration = isAudio ? _audioDurationService.GetDuration(url) : null;

        var blobEntity = new BlobFile
        {
            Id = newBlobId,
            ContainerName = containerName,
            StorageKey = newBlobName,
            Extension = fileExtension,
            ContentType = blobRequestDto.ContentType,
            Length = blobRequestDto.Length,
            Url = url,
            Duration = audioDuration,
            OwnerId = ownerId
        };

        var blobDb = await _unitOfWork.BlobRepository.Add(blobEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<BlobDto>(blobDb);
    }
    
    private void ValidateFileSize(long fileSize, bool isSong)
    {
        var maxFileSize = isSong ? _fileStorageSettings.MaxAudioFileSize : _fileStorageSettings.MaxImageFileSize;

        if (fileSize == 0) throw new InvalidOperationException("No file uploaded.");
        if (fileSize > maxFileSize)
            throw new InvalidOperationException("File size is too large.");
    }

    private string ValidateFileExtension(string fileName, string contentType, bool isSong)
    {
        var fileExtension = Path.GetExtension(fileName).ToLower();
        var allowedExtensions = isSong ? _fileStorageSettings.AllowedAudioExtensions : _fileStorageSettings.AllowedImageExtensions;
        var allowedType = isSong ? "audio" : "image";

        if (!allowedExtensions.Contains(fileExtension))
            throw new InvalidOperationException("File extension is not allowed.");
        if (!contentType.StartsWith($"{allowedType}/"))
            throw new InvalidOperationException($"File is not an {allowedType}.");
        
        return fileExtension;
    }
}
