using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Commands;

public record DeleteAlbum(Guid Id) : IRequest<bool>;

public class DeleteAlbumHandler(IUnitOfWork unitOfWork, ILoggingProvider logger) : IRequestHandler<DeleteAlbum, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingProvider _logger = logger;

    public async Task<bool> Handle(DeleteAlbum request, CancellationToken cancellationToken)
    {
        var albumToDelete = await _unitOfWork.AlbumRepository.GetById(request.Id, cancellationToken);
        if (albumToDelete == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.AlbumRepository.Delete(albumToDelete.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            _logger.LogInformation("Successfully deleted {EntityType} with Id {EntityId}.", nameof(Album), albumToDelete.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogError("Failed to delete {EntityType}. Error: {Error}", nameof(Album), ex.ToString());
            throw;
        }

        return true;
    }
}