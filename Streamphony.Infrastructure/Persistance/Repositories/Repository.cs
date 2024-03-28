using Microsoft.EntityFrameworkCore;

using Streamphony.Domain.Models;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Streamphony.Infrastructure.Persistence.Repositories
{
    public class Repository : IRepository
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById<TEntity>(Guid id) where TEntity : BaseEntity
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> GetByIdWithInclude<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _context.Set<TEntity>().Add(entity);
        }

        public async Task<TEntity> Delete<TEntity>(Guid id) where TEntity : BaseEntity
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"Object of type {typeof(TEntity)} with id {id} not found");
            }

            _context.Set<TEntity>().Remove(entity);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<TEntity> IncludeProperties<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
    }
}
