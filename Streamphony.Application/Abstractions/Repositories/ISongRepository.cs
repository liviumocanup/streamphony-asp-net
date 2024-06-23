using System.Linq.Expressions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface ISongRepository : IRepository<Song>
{
    Task DeleteWhere(Expression<Func<Song, bool>> predicate, CancellationToken cancellationToken);
    Task<IEnumerable<Song>> GetByOwnerId(Guid ownerId, CancellationToken cancellationToken);
    Task<IEnumerable<Song>> GetByOwnerIdWithBlobs(Guid ownerId, CancellationToken cancellationToken);
    Task<Song?> GetByOwnerIdAndTitle(Guid ownerId, string title, CancellationToken cancellationToken);

    Task<IEnumerable<Song>> GetByOwnerIdAndTitleWhereIdNotEqual(Guid ownerId, string title, Guid songId,
        CancellationToken cancellationToken);
    
    Task<IEnumerable<Song>> GetByIdsWithBlobs(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<Song?> GetByIdWithBlobs(Guid songId, CancellationToken cancellationToken);
}
