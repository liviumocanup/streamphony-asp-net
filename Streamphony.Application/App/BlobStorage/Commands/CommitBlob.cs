﻿using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.BlobStorage.Commands;

public record CommitBlob(Guid BlobId, Guid UserId, BlobType BlobType) : IRequest<bool>;

public class CommitBlobHandler(
    IValidationService validationService,
    IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService,
    IUserManagerProvider userManagerProvider) : IRequestHandler<CommitBlob, bool>
{
    private readonly IValidationService _validationService = validationService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBlobStorageService _blobStorage = blobStorageService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<bool> Handle(CommitBlob request, CancellationToken cancellationToken)
    {
        var blob = await _validationService.GetExistingEntity(_unitOfWork.BlobRepository, request.BlobId,
            cancellationToken);
        var userDb =
            await _validationService.GetExistingEntity(_userManagerProvider, request.UserId, cancellationToken);

        var artistId = userDb.ArtistId;
        await _validationService.AssertNavigationEntityExists<BlobFile, Artist>(_unitOfWork.ArtistRepository, artistId,
            cancellationToken, isNavRequired: true);
        if (blob.OwnerId != artistId) throw new UnauthorizedAccessException("You are not the owner of this blob");

        if (blob.ContainerName != BlobContainer.Draft) throw new InvalidOperationException("Blob is already committed");
        ValidateContentType(blob.ContentType, request.BlobType);
        
        var destinationContainer = GetDestinationContainer(request.BlobType);
        var destinationName = $"{GetDestinationName(request.BlobType)}/{blob.Id}";
        
        await _blobStorage.MoveBlobAsync(blob.ContainerName, blob.StorageKey, destinationContainer, destinationName);

        blob.ContainerName = destinationContainer;
        blob.StorageKey = destinationName;
        await _unitOfWork.SaveAsync(cancellationToken);

        return true;
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
    
    private static string GetDestinationName(BlobType blobType)
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
