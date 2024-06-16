using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface IArtistRepository : IRepository<Artist>
{
    Task<Artist?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
