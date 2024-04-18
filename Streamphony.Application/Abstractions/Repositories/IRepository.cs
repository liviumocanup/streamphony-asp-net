using System.Linq.Expressions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetById(Guid id);
        Task<T?> GetByIdWithInclude(Guid id, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task Delete(Guid id);
        Task<T> Update(T entity);
        // Task<PaginatedResult<TDto>> GetPagedData<TDto>(PagedRequest pagedRequest) where TDto : class;
    }
}