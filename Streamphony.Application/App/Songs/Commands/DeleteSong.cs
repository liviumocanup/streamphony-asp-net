using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record DeleteSong(Guid Id) : IRequest<bool>;

public class DeleteSongHandler(IUnitOfWork unitOfWork, ILoggingProvider logger) : IRequestHandler<DeleteSong, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingProvider _logger = logger;

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

            _logger.LogInformation("Successfully deleted {EntityType} with Id {EntityId}.", nameof(Song), songToDelete.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogError("Failed to delete {EntityType}. Error: {Error}", nameof(Song), ex.ToString());
            throw;
        }

        return true;
    }
}