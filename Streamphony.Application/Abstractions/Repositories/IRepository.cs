using System.Linq.Expressions;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetById(Guid id, CancellationToken cancellationToken);

    Task<T?> GetByIdWithInclude(Guid id, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includeProperties);

    Task<List<T>> GetAll(CancellationToken cancellationToken);

    Task<List<T>> GetAllWithInclude(CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includeProperties);

    Task<T> Add(T entity, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    Task<T> Update(T entity, CancellationToken cancellationToken);

    Task<(PaginatedResult<TDto>, IEnumerable<T>)> GetAllPaginated<TDto>(PagedRequest pagedRequest,
        CancellationToken cancellationToken, Func<IQueryable<T>, IQueryable<T>>? include = null) where TDto : class;
}
