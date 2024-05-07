using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Exceptions;
using System.Linq.Expressions;

namespace Streamphony.Application.Services;

public class ValidationService(IUnitOfWork unitOfWork, ILoggingProvider logger) : IValidationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingProvider _logger = logger;

    public async Task<TEntity> GetExistingEntity<TEntity>(IRepository<TEntity> repository, Guid entityId, CancellationToken cancellationToken, LogAction logAction) where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await repository.GetById(entityId, cancellationToken);
        if (existingEntity == null)
        {
            LogAndThrowNotFoundException(entityName, entityId, logAction);
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
            LogAndThrowNotFoundException(entityName, entityId, logAction);
        }
        return existingEntity!;
    }


    public async Task AssertEntityExists<TEntity>(IRepository<TEntity> repository, Guid entityId, CancellationToken cancellationToken, LogAction logAction) where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await repository.GetById(entityId, cancellationToken);
        if (existingEntity == null)
        {
            LogAndThrowNotFoundException(entityName, entityId, logAction);
        }
    }

    public async Task AssertNavigationEntityExists<TEntity, TNav>(IRepository<TNav> repository, Guid navId, CancellationToken cancellationToken, LogAction logAction) where TNav : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var navName = typeof(TNav).Name;
        var existingNav = await repository.GetById(navId, cancellationToken);
        if (existingNav == null)
        {
            LogAndThrowNotFoundExceptionForNavigation(entityName, navName, navId, logAction);
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
            LogAndThrowDuplicateException(entityName, propertyName, propertyValue, logAction);
        }
    }

    public async Task EnsureUniquePropertyExceptId<TEntity, TProperty>(Func<TProperty, Guid, CancellationToken, Task<IEnumerable<TEntity>>> getEntitiesFunc, string propertyName, TProperty propertyValue, Guid entityId, CancellationToken cancellationToken, LogAction logAction)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntities = await getEntitiesFunc(propertyValue, entityId, cancellationToken);
        if (existingEntities.Any())
        {
            LogAndThrowDuplicateException(entityName, propertyName, propertyValue, logAction);
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
            LogAndThrowDuplicateExceptionForUser(entityName, propertyName, propertyValue, ownerId, logAction);
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
            LogAndThrowDuplicateExceptionForUser(entityName, propertyName, propertyValue, ownerId, logAction);
        }
    }

    public void LogAndThrowNotAuthorizedException(string entityName, Guid entityId, string navName, Guid navId, LogAction logAction)
    {
        _logger.LogWarning("{LogAction} attempt for non-owned {EntityType} with Id '{EntityId}' by {NavigationType} with Id '{NavigationId}'.", logAction, entityName, entityId, navName, navId);
        throw new UnauthorizedException($"{navName} with Id '{navId}' does not own {entityName} with Id '{entityId}'.");
    }

    public void LogAndThrowDuplicateException<T>(string entityName, string propertyName, T propertyValue, LogAction logAction)
    {
        _logger.LogWarning("{LogAction} attempt for {EntityType} with existing {DuplicateProperty} '{PropertyValue}'.", logAction, entityName, propertyName, propertyValue);
        throw new DuplicateException($"{entityName} with {propertyName} '{propertyValue}' already exists.");
    }

    private void LogAndThrowNotFoundException(string entityName, Guid entityId, LogAction logAction)
    {
        _logger.LogWarning("{LogAction} attempt for non-existing {EntityType} with Id '{EntityId}'.", logAction, entityName, entityId);
        throw new NotFoundException($"{entityName} with Id '{entityId}' not found.");
    }

    private void LogAndThrowNotFoundExceptionForNavigation(string entityName, string navName, Guid navId, LogAction logAction)
    {
        _logger.LogWarning("{LogAction} attempt for {EntityType} for non-existing {NavigationType} with Id '{NavigationId}'.", logAction, entityName, navName, navId);
        throw new NotFoundException($"{navName} with Id '{navId}' not found.");
    }

    private void LogAndThrowDuplicateExceptionForUser<T>(string entityName, string propertyName, T propertyValue, Guid ownerId, LogAction logAction)
    {
        _logger.LogWarning("{LogAction} attempt for {EntityType} with existing {DuplicateProperty} '{PropertyValue}' for {NavigationType} with Id '{NavigationId}'.", logAction, entityName, propertyName, propertyValue, nameof(User), ownerId);
        throw new DuplicateException($"{entityName} with {propertyName} '{propertyValue}' already exists for User with Id '{ownerId}'.");
    }
}