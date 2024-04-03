using MediatR;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record DeleteSong(Guid Id) : IRequest<bool>;

public class DeleteSongHandler(IRepository<Song> repository, ILoggingService loggingService) : IRequestHandler<DeleteSong, bool>
{
    private readonly IRepository<Song> _repository = repository;
    private readonly ILoggingService _loggingService = loggingService;

    public async Task<bool> Handle(DeleteSong request, CancellationToken cancellationToken)
    {
        Guid id = request.Id;
        try
        {
            var songToDelete = await _repository.GetById(id);
            if (songToDelete == null) return false;

            await _repository.Delete(id);
            await _repository.SaveChangesAsync();
            await _loggingService.LogAsync($"Song id {id} - deleted successfully");

            return true;
        }
        catch (Exception ex)
        {
            await _loggingService.LogAsync($"Error deleting song id {id}: {ex.Message}");
            throw;
        }
    }
}