using System.Linq.Expressions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Interfaces.Repositories
{
    public interface IRepository
    {
        Task<TEntity> GetById<TEntity>(Guid id) where TEntity : BaseEntity;
        Task<TEntity> GetByIdWithInclude<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity;
        Task<List<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity;
        Task SaveChangesAsync();
        void Add<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<TEntity> Delete<TEntity>(Guid id) where TEntity : BaseEntity;
        // Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : BaseEntity
        //                                                                                      where TDto : class;
    }
}