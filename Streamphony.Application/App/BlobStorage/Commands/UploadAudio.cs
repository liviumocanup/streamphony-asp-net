using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Configurations;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.DTOs;

namespace Streamphony.Application.App.BlobStorage.Commands;

public record UploadAudio(Guid SongId, Stream FileStream, long FileSize, string FileName) : IRequest<SongDto>;

public class UploadAudioHandler(
    IBlobStorageService blobStorageService,
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    IFileStorageSettings fileStorageSettings,
    IAudioDurationService audioDurationService)
    : IRequestHandler<UploadAudio, SongDto>
{
    private readonly IBlobStorageService _blobStorage = blobStorageService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IFileStorageSettings _fileStorageSettings = fileStorageSettings;
    private readonly IAudioDurationService _audioDurationService = audioDurationService;

    public async Task<SongDto> Handle(UploadAudio request, CancellationToken cancellationToken)
    {
        var songId = request.SongId;
        var fileStream = request.FileStream;
        
        ValidateFile(request.FileSize, request.FileName);

        var url = await _blobStorage.UploadAudioFileAsync(songId.ToString(), fileStream);
        if (string.IsNullOrWhiteSpace(url)) throw new Exception("Failed to upload song file");
        
        var duration = _audioDurationService.GetDuration(url);
        var songDb = await _unitOfWork.SongRepository.GetById(songId, cancellationToken);
        songDb!.Url = url;
        songDb.Duration = duration;

        await _unitOfWork.SaveAsync(cancellationToken);
        return _mapper.Map<SongDto>(songDb);
    }
    
    private void ValidateFile(long fileSize, string fileName)
    {
        var fileExtension = Path.GetExtension(fileName).ToLower();
        var fileSizeInMb = fileSize / 1024 / 1024;
        
        Console.WriteLine($"File size: {fileSize}");
        Console.WriteLine($"File extension: {fileExtension}");
        Console.WriteLine($"Max file size: {_fileStorageSettings.MaxAudioFileSize}");
        Console.WriteLine($"Allowed extensions: {_fileStorageSettings.AllowedAudioExtensions}");
 
        if (fileSize == 0) throw new InvalidOperationException("No file uploaded.");
        if (fileSizeInMb > _fileStorageSettings.MaxAudioFileSize)
            throw new InvalidOperationException("File size is too large.");
        if (!_fileStorageSettings.AllowedAudioExtensions.Contains(fileExtension))
            throw new InvalidOperationException("Invalid file type.");
    }
}
