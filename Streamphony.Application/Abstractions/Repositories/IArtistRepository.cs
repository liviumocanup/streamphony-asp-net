using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface IArtistRepository : IRepository<Artist>
{
    Task<Artist?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Artist?> GetByOwnerIdWithBlobs(Guid ownerId, CancellationToken cancellationToken);
    Task<Artist?> GetByIdWithBlobs(Guid artistId, CancellationToken cancellationToken);
}
