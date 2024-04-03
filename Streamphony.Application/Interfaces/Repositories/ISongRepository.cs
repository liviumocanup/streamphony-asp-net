using Streamphony.Domain.Models;

namespace Streamphony.Application.Interfaces.Repositories
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<IEnumerable<Song>> GetSongsByGenreAsync(Guid genreId);
    }
}
