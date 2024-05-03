using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;

namespace Streamphony.Application.App.Albums.Commands;

public record DeleteAlbum(Guid Id) : IRequest<bool>;

public class DeleteAlbumHandler(IUnitOfWork unitOfWork, ILoggingService loggingService) : IRequestHandler<DeleteAlbum, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingService _loggingService = loggingService;

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

            await _loggingService.LogAsync($"Album id {albumToDelete.Id} - deleted successfully");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Error deleting album id {albumToDelete.Id}: ", ex);
            throw;
        }

        return true;
    }
}