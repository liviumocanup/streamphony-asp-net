using System.Linq.Expressions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface ISongRepository : IRepository<Song>
{
    Task DeleteWhere(Expression<Func<Song, bool>> predicate, CancellationToken cancellationToken);
}
