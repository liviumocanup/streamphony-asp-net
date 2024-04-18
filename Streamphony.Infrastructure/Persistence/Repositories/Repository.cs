using Microsoft.EntityFrameworkCore;

using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Infrastructure.Persistence.Contexts;
using System.Linq.Expressions;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    // cancellation token
    public async Task<T?> GetById(Guid id /*, CancellationToken cancellationToken = default*/)
    {
        return await _context.FindAsync<T>(id /*, cancellationToken*/);
    }

    public async Task<T?> GetByIdWithInclude(Guid id, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        return (await query.FirstOrDefaultAsync(entity => entity.Id == id))!;
    }

    public async Task<T> Add(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id) ??
                        throw new ArgumentException($"Object of type {typeof(T)} with id {id} not found");

        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task<T> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    private IQueryable<T> IncludeProperties(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return query;
    }
}