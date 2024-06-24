using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Extensions;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }
    
    public async Task<List<T>> GetAllWithInclude(CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.FindAsync<T>([id], cancellationToken);
    }

    public async Task<T?> GetByIdWithInclude(Guid id, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        return await query.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<T> Add(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<T>().FindAsync([id], cancellationToken) ??
                     throw new ArgumentException($"Object of type {typeof(T)} with id {id} not found");

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> Update(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<(PaginatedResult<TDto>, IEnumerable<T>)> GetAllPaginated<TDto>(PagedRequest pagedRequest,
        CancellationToken cancellationToken, Func<IQueryable<T>, IQueryable<T>>? include = null)
        where TDto : class
    {
        return await _context.Set<T>().CreatePaginatedResultAsync<T, TDto>(pagedRequest, cancellationToken, include);
    }

    private IQueryable<T> IncludeProperties(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
        return query;
    }
}
