using Streamphony.Application.Common.Enum;

namespace Streamphony.Application.Abstractions.Services;

public interface ILoggingService
{
    void LogSuccess(string entityName, LogAction logAction = LogAction.Get);
    void LogSuccess(string entityName, Guid entityId, LogAction logAction = LogAction.Create);
    void LogAndThrowNotAuthorizedException(string entityName, LogAction logAction = LogAction.Login);

    void LogAndThrowNotAuthorizedException(string entityName, Guid entityId, string navName, Guid navId,
        LogAction logAction = LogAction.Update);

    void LogAndThrowNotFoundException(string entityName, Guid entityId, LogAction logAction);

    void LogAndThrowNotFoundException<T>(string entityName, string propertyName, T propertyValue,
        LogAction logAction = LogAction.Login);

    void LogAndThrowNotFoundExceptionForNavigation(string entityName, string navName, Guid navId,
        LogAction logAction = LogAction.Create);

    void LogAndThrowDuplicateException<T>(string entityName, string propertyName, T propertyValue,
        LogAction logAction = LogAction.Update);

    void LogAndThrowDuplicateExceptionForArtist<T>(string entityName, string propertyName, T propertyValue,
        Guid ownerId, LogAction logAction);
}
