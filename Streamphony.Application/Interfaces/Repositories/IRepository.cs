using System.Linq.Expressions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(Guid id);
        Task<TEntity> GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetAll();
        Task SaveChangesAsync();
        void Add(TEntity entity);
        Task<TEntity> Delete(Guid id);
        void Update(TEntity entity);
        // Task<PaginatedResult<TDto>> GetPagedData<TDto>(PagedRequest pagedRequest) where TDto : class;
    }
}