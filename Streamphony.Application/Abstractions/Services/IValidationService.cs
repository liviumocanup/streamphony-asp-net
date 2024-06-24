using System.Linq.Expressions;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.Abstractions.Services;

public interface IValidationService
{
    Task<TEntity> GetExistingEntity<TEntity>(
        IRepository<TEntity> repository,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Update
    ) where TEntity : BaseEntity;
    
    Task<User> GetExistingEntity(
        IUserManagerProvider repository,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Create
    );

    Task<TEntity> GetExistingEntityWithInclude<TEntity>(
        Func<Guid, CancellationToken, Expression<Func<TEntity, object>>[], Task<TEntity?>> getEntityWithIncludeFunc,
        Guid entityId,
        LogAction logAction,
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] includeProperties
    ) where TEntity : BaseEntity;

    Task AssertEntityExists<TEntity>(
        IRepository<TEntity> repository,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Delete
    ) where TEntity : BaseEntity;

    Task AssertNavigationEntityExists<TEntity, TNav>(
        IRepository<TNav> repository,
        Guid navId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Create
    ) where TNav : BaseEntity;

    Task AssertNavigationEntityExists<TEntity, TNav>(
        IRepository<TNav> repository,
        Guid? id,
        CancellationToken cancellationToken,
        bool isNavRequired = false,
        LogAction logAction = LogAction.Create
    ) where TNav : BaseEntity;

    Task EnsureUniqueProperty<TEntity, TProperty>(
        Func<TProperty, CancellationToken, Task<TEntity?>> getEntityFunc,
        string propertyName,
        TProperty propertyValue,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Create);

    Task EnsureUniquePropertyExceptId<TEntity, TProperty>(
        Func<TProperty, Guid, CancellationToken, Task<IEnumerable<TEntity>>> getEntitiesFunc,
        string propertyName,
        TProperty propertyValue,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Update);

    Task EnsureArtistUniqueProperty<TEntity, TProperty>(
        Func<Guid, TProperty, CancellationToken, Task<TEntity?>> getEntityFunc,
        Guid ownerId,
        string propertyName,
        TProperty propertyValue,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Create);

    Task EnsureArtistUniquePropertyExceptId<TEntity, TProperty>(
        Func<Guid, TProperty, Guid, CancellationToken, Task<IEnumerable<TEntity>>> getEntitiesFunc,
        Guid ownerId,
        string propertyName,
        TProperty propertyValue,
        Guid entityId,
        CancellationToken cancellationToken,
        LogAction logAction = LogAction.Update);
}
