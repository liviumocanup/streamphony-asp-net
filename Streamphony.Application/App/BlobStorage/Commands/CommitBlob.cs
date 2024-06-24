using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.BlobStorage.Commands;

public record CommitBlob(Guid BlobId, Guid UserId, Guid RelatedEntityId, string BlobType) : IRequest<string>;

public class CommitBlobHandler(
    IValidationService validationService,
    IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService,
    IUserManagerProvider userManagerProvider) : IRequestHandler<CommitBlob, string>
{
    private readonly IValidationService _validationService = validationService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBlobStorageService _blobStorage = blobStorageService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<string> Handle(CommitBlob request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<BlobType>(request.BlobType, true, out var blobType))
            throw new InvalidOperationException("Invalid blob type.");
        
        var isProfilePicture = blobType == BlobType.ProfilePicture;
        var relatedEntityId = request.RelatedEntityId;
        
        var blob = await _validationService.GetExistingEntity(_unitOfWork.BlobRepository, request.BlobId,
            cancellationToken);
        var userDb =
            await _validationService.GetExistingEntity(_userManagerProvider, request.UserId, cancellationToken);

        var ownerId = userDb.Id;
        if (!isProfilePicture)
        {
            var artistId = userDb.ArtistId;
            await _validationService.AssertNavigationEntityExists<User, Artist>(_unitOfWork.ArtistRepository, artistId, cancellationToken, isNavRequired: true);
            ownerId = artistId!.Value;
        }
        
        if (blob.OwnerId != ownerId) throw new UnauthorizedAccessException($"You are not the owner of this {blobType} blob");
        if (blob.ContainerName != BlobContainer.Draft) throw new InvalidOperationException("Blob is already committed");
        
        ValidateContentType(blob.ContentType, blobType);
        
        var destinationContainer = GetDestinationContainer(blobType);
        var destinationName = $"{GetDestinationFolder(blobType)}/{blob.Id}";
        
        var destinationUrl = await _blobStorage.MoveBlobAsync(blob.ContainerName, blob.StorageKey, destinationContainer, destinationName);

        blob.ContainerName = destinationContainer;
        blob.StorageKey = destinationName;
        blob.Url = destinationUrl;
        blob.RelatedEntityId = relatedEntityId;
        await _unitOfWork.SaveAsync(cancellationToken);

        return destinationUrl;
    }
    
    private static void ValidateContentType(string contentType, BlobType blobType)
    {
        var expectedTypePrefix = blobType switch
        {
            BlobType.Song => "audio",
            BlobType.ProfilePicture => "image",
            BlobType.SongCover => "image",
            BlobType.AlbumCover => "image",
            _ => throw new ArgumentException("Unsupported blob type")
        };

        if (!contentType.StartsWith($"{expectedTypePrefix}/", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException($"Content type '{contentType}' does not match expected type '{expectedTypePrefix}'.");
    }

    private static string GetDestinationContainer(BlobType blobType)
    {
        return blobType switch
        {
            BlobType.Song => BlobContainer.Songs,
            BlobType.ProfilePicture => BlobContainer.Images,
            BlobType.SongCover => BlobContainer.Images,
            BlobType.AlbumCover => BlobContainer.Images,
            _ => throw new ArgumentException("Unsupported blob type")
        };
    }
    
    private static string GetDestinationFolder(BlobType blobType)
    {
        return blobType switch
        {
            BlobType.Song => BlobContainer.Songs,
            BlobType.ProfilePicture => BlobContainer.ProfilePictures,
            BlobType.SongCover => BlobContainer.SongCovers,
            BlobType.AlbumCover => BlobContainer.AlbumCovers,
            _ => throw new ArgumentException("Unsupported blob type")
        };
    }
}
