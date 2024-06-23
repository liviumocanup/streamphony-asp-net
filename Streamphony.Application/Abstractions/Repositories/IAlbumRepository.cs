using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface IAlbumRepository : IRepository<Album>
{
    Task<Album?> GetByTitle(string title, CancellationToken cancellationToken);
    Task<Album?> GetByOwnerIdAndTitle(Guid ownerId, string title, CancellationToken cancellationToken);

    Task<IEnumerable<Album>> GetByOwnerIdAndTitleWhereIdNotEqual(Guid ownerId, string title, Guid albumId,
        CancellationToken cancellationToken);
    
    Task<IEnumerable<Album>> GetByOwnerIdWithBlobs(Guid ownerId, CancellationToken cancellationToken);
    Task<Album?> GetByIdWithBlobs(Guid albumId, CancellationToken cancellationToken);
}
