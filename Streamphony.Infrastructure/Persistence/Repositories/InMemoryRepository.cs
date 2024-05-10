using System.Linq.Expressions;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions.Repositories;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly List<T> _entities = [];

    public Task<T?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = _entities.Find(e => e.Id == id);
        return Task.FromResult(entity);
    }

    public Task<T?> GetByIdWithInclude(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includeProperties)
    {
        return GetById(id, cancellationToken);
    }

    public Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult(_entities);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<T> Add(T entity, CancellationToken cancellationToken)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _entities.Add(entity);
        return Task.FromResult(entity);
    }

    public Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = _entities.Find(e => e.Id == id) ?? throw new KeyNotFoundException($"Entity with id {id} not found");
        _entities.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<T> Update(T entity, CancellationToken cancellationToken)
    {
        var existingEntity = _entities.Find(e => e.Id == entity.Id) ?? throw new KeyNotFoundException($"Entity with id {entity.Id} not found");
        _entities.Remove(existingEntity);
        _entities.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<(List<T>, int)> GetAllPaginated(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var totalRecords = _entities.Count;
        var items = _entities.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
        return Task.FromResult((items, totalRecords));
    }
}
