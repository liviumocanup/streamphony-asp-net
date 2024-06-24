using System.Linq.Expressions;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.Services;

public class ValidationService(ILoggingService loggingService) : IValidationService
{
    private readonly ILoggingService _loggingService = loggingService;

    public async Task<TEntity> GetExistingEntity<TEntity>(
        IRepository<TEntity> repository,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Update
    ) where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await repository.GetById(entityId, cancellationToken);

        if (existingEntity == null)
            _loggingService.LogAndThrowNotFoundException(entityName, entityId, logAction);

        return existingEntity!;
    }

    public async Task<User> GetExistingEntity(
        IUserManagerProvider repository,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Create
    )
    {
        const string entityName = nameof(User);
        var existingEntity = await repository.FindByIdAsync(entityId.ToString());

        if (existingEntity == null)
            _loggingService.LogAndThrowNotFoundException(entityName, entityId, logAction);

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
            _loggingService.LogAndThrowNotFoundException(entityName, entityId, logAction);

        return existingEntity!;
    }


    public async Task AssertEntityExists<TEntity>(
        IRepository<TEntity> repository,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Delete
    ) where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await repository.GetById(entityId, cancellationToken);

        if (existingEntity == null)
            _loggingService.LogAndThrowNotFoundException(entityName, entityId, logAction);
    }

    public async Task AssertNavigationEntityExists<TEntity, TNav>(
        IRepository<TNav> repository,
        Guid navId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Create
    ) where TNav : BaseEntity
    {
        var entityName = typeof(TEntity).Name;
        var navName = typeof(TNav).Name;
        var existingNav = await repository.GetById(navId, cancellationToken);

        if (existingNav == null)
            _loggingService.LogAndThrowNotFoundExceptionForNavigation(entityName, navName, navId, logAction);
    }

    public async Task AssertNavigationEntityExists<TEntity, TNav>(
        IRepository<TNav> repository,
        Guid? id,
        CancellationToken cancellationToken,
        bool isNavRequired = false,
        LogAction logAction = LogAction.Create
    ) where TNav : BaseEntity
    {
        if (id == null)
            if (isNavRequired)
                _loggingService.LogAndThrowNotFoundExceptionForNavigation(typeof(TEntity).Name, typeof(TNav).Name,
                    Guid.Empty, logAction);
            else return;

        var navId = id!.Value;

        await AssertNavigationEntityExists<TEntity, TNav>(repository, navId, cancellationToken, logAction);
    }

    public async Task EnsureUniqueProperty<TEntity, TProperty>(
        Func<TProperty, CancellationToken, Task<TEntity?>> getEntityFunc,
        string propertyName,
        TProperty propertyValue,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Create)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await getEntityFunc(propertyValue, cancellationToken);

        if (existingEntity != null)
            _loggingService.LogAndThrowDuplicateException(entityName, propertyName, propertyValue, logAction);
    }

    public async Task EnsureUniquePropertyExceptId<TEntity, TProperty>(
        Func<TProperty, Guid, CancellationToken, Task<IEnumerable<TEntity>>> getEntitiesFunc,
        string propertyName,
        TProperty propertyValue,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Update)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntities = await getEntitiesFunc(propertyValue, entityId, cancellationToken);

        if (existingEntities.Any())
            _loggingService.LogAndThrowDuplicateException(entityName, propertyName, propertyValue, logAction);
    }

    public async Task EnsureArtistUniqueProperty<TEntity, TProperty>(
        Func<Guid, TProperty, CancellationToken, Task<TEntity?>> getEntityFunc,
        Guid ownerId,
        string propertyName,
        TProperty propertyValue,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Create)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntity = await getEntityFunc(ownerId, propertyValue, cancellationToken);

        if (existingEntity != null)
            _loggingService.LogAndThrowDuplicateExceptionForArtist(entityName, propertyName, propertyValue, ownerId,
                logAction);
    }

    public async Task EnsureArtistUniquePropertyExceptId<TEntity, TProperty>(
        Func<Guid, TProperty, Guid, CancellationToken, Task<IEnumerable<TEntity>>> getEntitiesFunc,
        Guid ownerId,
        string propertyName,
        TProperty propertyValue,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Update)
    {
        var entityName = typeof(TEntity).Name;
        var existingEntities = await getEntitiesFunc(ownerId, propertyValue, entityId, cancellationToken);

        if (existingEntities.Any())
            _loggingService.LogAndThrowDuplicateExceptionForArtist(entityName, propertyName, propertyValue, ownerId,
                logAction);
    }
}
