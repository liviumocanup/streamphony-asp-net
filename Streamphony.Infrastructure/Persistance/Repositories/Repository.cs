using Microsoft.EntityFrameworkCore;

using Streamphony.Domain.Models;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Streamphony.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity>(ApplicationDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context = context;

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        // cancellation token
        public async Task<TEntity> GetById(Guid id /*, CancellationToken cancellationToken = default*/)
        {
            return (await _context.FindAsync<TEntity>(id /*, cancellationToken*/))!;
        }

        public async Task<TEntity> GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = IncludeProperties(includeProperties);
            return (await query.FirstOrDefaultAsync(entity => entity.Id == id))!;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public async Task<TEntity> Delete(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id) ??
                            throw new ArgumentException($"Object of type {typeof(TEntity)} with id {id} not found");
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
