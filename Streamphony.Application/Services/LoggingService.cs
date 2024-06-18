using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Services;

public class LoggingService(ILoggingProvider logger) : ILoggingService
{
    private readonly ILoggingProvider _logger = logger;

    public void LogSuccess(string entityName, LogAction logAction = LogAction.Get)
    {
        _logger.LogInformation("{LogAction} success for all {EntityType}s.", logAction, entityName);
    }

    public void LogSuccess(string entityName, Guid entityId, LogAction logAction = LogAction.Create)
    {
        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", logAction, entityName,
            entityId);
    }

    public void LogAndThrowNotAuthorizedException(string entityName, LogAction logAction = LogAction.Login)
    {
        _logger.LogWarning("{LogAction} attempt for {EntityType} with invalid credentials.", logAction, entityName);
        throw new UnauthorizedException($"Invalid credentials for {entityName}.");
    }

    public void LogAndThrowNotAuthorizedException(string entityName, Guid entityId, string navName, Guid navId,
        LogAction logAction = LogAction.Update)
    {
        _logger.LogWarning(
            "{LogAction} attempt for non-owned {EntityType} with Id '{EntityId}' by {NavigationType} with Id '{NavigationId}'.",
            logAction, entityName, entityId, navName, navId);
        throw new UnauthorizedException($"{navName} with Id '{navId}' does not own {entityName} with Id '{entityId}'.");
    }

    public void LogAndThrowNotFoundException(string entityName, Guid entityId, LogAction logAction = LogAction.Delete)
    {
        _logger.LogWarning("{LogAction} attempt for non-existing {EntityType} with Id '{EntityId}'.", logAction,
            entityName, entityId);
        throw new NotFoundException($"{entityName} with Id '{entityId}' not found.");
    }

    public void LogAndThrowNotFoundException<T>(string entityName, string propertyName, T propertyValue,
        LogAction logAction)
    {
        _logger.LogWarning("{LogAction} attempt for non-existing {EntityType} with {Property} '{PropertyValue}'.",
            logAction, entityName, propertyName, propertyValue);
        throw new NotFoundException($"{entityName} with {propertyName} '{propertyValue}' not found.");
    }

    public void LogAndThrowNotFoundExceptionForNavigation(string entityName, string navName, Guid navId,
        LogAction logAction)
    {
        _logger.LogWarning(
            "{LogAction} attempt for {EntityType} with non-existing {NavigationType} with Id '{NavigationId}'.",
            logAction, entityName, navName, navId);
        throw new NotFoundException($"{navName} with Id '{navId}' not found.");
    }

    public void LogAndThrowDuplicateException<T>(string entityName, string propertyName, T propertyValue,
        LogAction logAction = LogAction.Update)
    {
        _logger.LogWarning("{LogAction} attempt for {EntityType} with existing {DuplicateProperty} '{PropertyValue}'.",
            logAction, entityName, propertyName, propertyValue);
        throw new DuplicateException($"{entityName} with {propertyName} '{propertyValue}' already exists.");
    }

    public void LogAndThrowDuplicateExceptionForArtist<T>(string entityName, string propertyName, T propertyValue,
        Guid ownerId, LogAction logAction)
    {
        _logger.LogWarning(
            "{LogAction} attempt for {EntityType} with existing {DuplicateProperty} '{PropertyValue}' for {NavigationType} with Id '{NavigationId}'.",
            logAction, entityName, propertyName, propertyValue, nameof(Artist), ownerId);
        throw new DuplicateException(
            $"{entityName} with {propertyName} '{propertyValue}' already exists for Artist with Id '{ownerId}'.");
    }
}
