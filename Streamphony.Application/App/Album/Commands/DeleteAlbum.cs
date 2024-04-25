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
        var albumToDelete = await _unitOfWork.AlbumRepository.GetById(request.Id);
        if (albumToDelete == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.AlbumRepository.Delete(albumToDelete.Id);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            await _loggingService.LogAsync($"Album id {albumToDelete.Id} - deleted successfully");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();

            await _loggingService.LogAsync($"Error deleting album id {albumToDelete.Id}: ", ex);
            throw;
        }

        return true;
    }
}