using Streamphony.Domain.Models;

namespace Streamphony.Application.Interfaces.Repositories
{
    public interface ISongRepository : IRepository
    {
        Task<IEnumerable<Song>> GetSongsByGenreAsync(Guid genreId);
    }
}
