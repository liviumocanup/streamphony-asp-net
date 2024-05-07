using Streamphony.Application.Services;

namespace Streamphony.Application.Abstractions.Services;

public interface ILoggingService
{
    void LogSuccess(string entityName, LogAction logAction = LogAction.Get);
    void LogSuccess(string entityName, Guid entityId, LogAction logAction = LogAction.Create);
    void LogAndThrowNotAuthorizedException(string entityName, Guid entityId, string navName, Guid navId, LogAction logAction = LogAction.Update);
    void LogAndThrowNotFoundException(string entityName, Guid entityId, LogAction logAction);
    void LogAndThrowNotFoundExceptionForNavigation(string entityName, string navName, Guid navId, LogAction logAction);
    void LogAndThrowDuplicateException<T>(string entityName, string propertyName, T propertyValue, LogAction logAction = LogAction.Update);
    void LogAndThrowDuplicateExceptionForUser<T>(string entityName, string propertyName, T propertyValue, Guid ownerId, LogAction logAction);
}