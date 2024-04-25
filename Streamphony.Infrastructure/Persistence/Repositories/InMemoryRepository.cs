using System.Linq.Expressions;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions.Repositories;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly List<T> _entities = [];

    public Task<T?> GetById(Guid id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entity);
    }

    public Task<T?> GetByIdWithInclude(Guid id, params Expression<Func<T, object>>[] includeProperties)
    {
        return GetById(id);
    }

    public Task<List<T>> GetAll()
    {
        return Task.FromResult(_entities);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }

    public Task<T> Add(T entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _entities.Add(entity);
        return Task.FromResult(entity);
    }

    public Task Delete(Guid id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id) ?? throw new KeyNotFoundException($"Entity with id {id} not found");
        _entities.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<T> Update(T entity)
    {
        var existingEntity = _entities.FirstOrDefault(e => e.Id == entity.Id) ?? throw new KeyNotFoundException($"Entity with id {entity.Id} not found");
        _entities.Remove(existingEntity);
        _entities.Add(entity);
        return Task.FromResult(entity);
    }
}
