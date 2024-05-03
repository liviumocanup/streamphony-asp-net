using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;

namespace Streamphony.Application.App.Songs.Commands;

public record DeleteSong(Guid Id) : IRequest<bool>;

public class DeleteSongHandler(IUnitOfWork unitOfWork, ILoggingService loggingService) : IRequestHandler<DeleteSong, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingService _loggingService = loggingService;

    public async Task<bool> Handle(DeleteSong request, CancellationToken cancellationToken)
    {
        var songToDelete = await _unitOfWork.SongRepository.GetById(request.Id, cancellationToken);
        if (songToDelete == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.SongRepository.Delete(songToDelete.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Song id {songToDelete.Id} - deleted successfully");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Error deleting song id {songToDelete.Id}: ", ex);
            throw;
        }

        return true;
    }
}