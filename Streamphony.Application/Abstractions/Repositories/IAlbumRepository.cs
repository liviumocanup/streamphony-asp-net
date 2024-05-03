using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface IAlbumRepository : IRepository<Album>
{
    Task<Album?> GetByTitle(string title, CancellationToken cancellationToken);
}
