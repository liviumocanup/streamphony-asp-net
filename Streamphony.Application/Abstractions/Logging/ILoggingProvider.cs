namespace Streamphony.Application.Abstractions.Logging;

public interface ILoggingProvider
{
    void LogInformation(string message);
    void LogInformation(string messageTemplate, params object?[]? propertyValues);
    void LogInformation(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    void LogWarning(string message);
    void LogWarning(string messageTemplate, params object?[]? propertyValues);
    void LogWarning(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    void LogError(string message, Exception ex);
    void LogError(string messageTemplate, params object?[]? propertyValues);
    void LogError(Exception? exception, string messageTemplate, params object?[]? propertyValues);
}
