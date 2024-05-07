using System.Linq.Expressions;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.Services;

public class ValidationService(ILoggingService loggingService) : IValidationService
{
    private readonly ILoggingService _loggingService = loggingService;

    public async Task<TEntity> GetExistingEntity<TEntity>(IRepository<TEntity> repository, Guid entityId, CancellationToken cancellationToken, LogAction logAction) where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await repository.GetById(entityId, cancellationToken);
        if (existingEntity == null)
        {
            _loggingService.LogAndThrowNotFoundException(entityName, entityId, logAction);
        }
        return existingEntity!;
    }

    public async Task<TEntity> GetExistingEntityWithInclude<TEntity>(
        Func<Guid, CancellationToken, Expression<Func<TEntity, object>>[], Task<TEntity?>> getEntityWithIncludeFunc,
        Guid entityId,
        LogAction logAction,
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] includeProperties
    ) where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await getEntityWithIncludeFunc(entityId, cancellationToken, includeProperties);
        if (existingEntity == null)
        {
            _loggingService.LogAndThrowNotFoundException(entityName, entityId, logAction);
        }
        return existingEntity!;
    }


    public async Task AssertEntityExists<TEntity>(IRepository<TEntity> repository, Guid entityId, CancellationToken cancellationToken, LogAction logAction) where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await repository.GetById(entityId, cancellationToken);
        if (existingEntity == null)
        {
            _loggingService.LogAndThrowNotFoundException(entityName, entityId, logAction);
        }
    }

    public async Task AssertNavigationEntityExists<TEntity, TNav>(IRepository<TNav> repository, Guid navId, CancellationToken cancellationToken, LogAction logAction) where TNav : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var navName = typeof(TNav).Name;
        var existingNav = await repository.GetById(navId, cancellationToken);
        if (existingNav == null)
        {
            _loggingService.LogAndThrowNotFoundExceptionForNavigation(entityName, navName, navId, logAction);
        }
    }

    public async Task AssertNavigationEntityExists<TEntity, TNav>(IRepository<TNav> repository, Guid? id, CancellationToken cancellationToken, LogAction logAction) where TNav : BaseEntity
    {
        if (id == null) return;
        var navId = id.Value;

        await AssertNavigationEntityExists<TEntity, TNav>(repository, navId, cancellationToken, logAction);
    }

    public async Task EnsureUniqueProperty<TEntity, TProperty>(Func<TProperty, CancellationToken, Task<TEntity?>> getEntityFunc, string propertyName, TProperty propertyValue, CancellationToken cancellationToken, LogAction logAction)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await getEntityFunc(propertyValue, cancellationToken);
        if (existingEntity != null)
        {
            _loggingService.LogAndThrowDuplicateException(entityName, propertyName, propertyValue, logAction);
        }
    }

    public async Task EnsureUniquePropertyExceptId<TEntity, TProperty>(Func<TProperty, Guid, CancellationToken, Task<IEnumerable<TEntity>>> getEntitiesFunc, string propertyName, TProperty propertyValue, Guid entityId, CancellationToken cancellationToken, LogAction logAction)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntities = await getEntitiesFunc(propertyValue, entityId, cancellationToken);
        if (existingEntities.Any())
        {
            _loggingService.LogAndThrowDuplicateException(entityName, propertyName, propertyValue, logAction);
        }
    }

    public async Task EnsureUserUniqueProperty<TEntity, TProperty>(
    Func<Guid, TProperty, CancellationToken, Task<TEntity?>> getEntityFunc,
    Guid ownerId, string propertyName, TProperty propertyValue, CancellationToken cancellationToken, LogAction logAction)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await getEntityFunc(ownerId, propertyValue, cancellationToken);
        if (existingEntity != null)
        {
            _loggingService.LogAndThrowDuplicateExceptionForUser(entityName, propertyName, propertyValue, ownerId, logAction);
        }
    }

    public async Task EnsureUserUniquePropertyExceptId<TEntity, TProperty>(
        Func<Guid, TProperty, Guid, CancellationToken, Task<IEnumerable<TEntity>>> getEntitiesFunc,
        Guid ownerId, string propertyName, TProperty propertyValue, Guid entityId, CancellationToken cancellationToken, LogAction logAction)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntities = await getEntitiesFunc(ownerId, propertyValue, entityId, cancellationToken);
        if (existingEntities.Any())
        {
            _loggingService.LogAndThrowDuplicateExceptionForUser(entityName, propertyName, propertyValue, ownerId, logAction);
        }
    }
}