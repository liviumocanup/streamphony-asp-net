using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.BlobStorage.Commands;

public record DeleteBlob(Guid Id, Guid UserId) : IRequest<Unit>;

public class DeleteBlobHandler(
    IBlobStorageService blobStorageService,
    IUnitOfWork unitOfWork,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider,
    ILoggingService logger) : IRequestHandler<DeleteBlob, Unit>
{
    private readonly IBlobStorageService _blobStorage = blobStorageService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly ILoggingService _logger = logger;

    public async Task<Unit> Handle(DeleteBlob request, CancellationToken cancellationToken)
    {
        var blobId = request.Id;
        var userId = request.UserId;

        var blobMetadataDb = await _validationService.GetExistingEntity(_unitOfWork.BlobRepository, blobId,
            cancellationToken, LogAction.Delete);
        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, userId, cancellationToken);
        var artistId = userDb.ArtistId;
        await _validationService.AssertNavigationEntityExists<BlobFile, Artist>(_unitOfWork.ArtistRepository,
            artistId, cancellationToken, isNavRequired: true);
        
        if (blobMetadataDb.OwnerId != artistId)
            _logger.LogAndThrowNotAuthorizedException(nameof(BlobFile), blobId, nameof(Artist), userId);

        await _unitOfWork.BlobRepository.Delete(blobId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        await _blobStorage.DeleteFileAsync(blobMetadataDb.ContainerName, blobMetadataDb.StorageKey);
        return Unit.Value;
    }
}
