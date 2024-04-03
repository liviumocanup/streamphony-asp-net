using System.Linq.Expressions;
using Streamphony.Domain.Models;
using Streamphony.Application.Interfaces.Repositories;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly List<TEntity> _entities = new();

    public Task<TEntity> GetById(Guid id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entity!);
    }

    public Task<TEntity> GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return GetById(id);
    }

    public Task<List<TEntity>> GetAll()
    {
        return Task.FromResult(_entities);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }

    public void Add(TEntity entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _entities.Add(entity);
    }

    public Task<TEntity> Delete(Guid id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        if (entity != null)
        {
            _entities.Remove(entity);
        }
        return Task.FromResult(entity!);
    }

    public void Update(TEntity entity)
    {
        var existingEntity = _entities.FirstOrDefault(e => e.Id == entity.Id);
        if (existingEntity != null)
        {
            _entities.Remove(existingEntity);
            _entities.Add(entity);
        }
    }
}
