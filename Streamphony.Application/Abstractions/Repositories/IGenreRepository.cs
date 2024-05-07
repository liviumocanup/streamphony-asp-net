using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task<Genre?> GetByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Genre>> GetByNameWhereIdNotEqual(string name, Guid genreId, CancellationToken cancellationToken);
}
