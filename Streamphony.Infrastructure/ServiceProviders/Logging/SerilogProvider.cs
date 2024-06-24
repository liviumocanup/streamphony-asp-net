using Serilog;
using Streamphony.Application.Abstractions.Logging;

namespace Streamphony.Infrastructure.ServiceProviders.Logging;

public class SerilogProvider : ILoggingProvider
{
    public void LogInformation(string message)
    {
        Log.Information(message);
    }

    public void LogInformation(string messageTemplate, params object?[]? propertyValues)
    {
        Log.Information(messageTemplate, propertyValues);
    }

    public void LogInformation(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Log.Information(exception, messageTemplate, propertyValues);
    }

    public void LogWarning(string message)
    {
        Log.Warning(message);
    }

    public void LogWarning(string messageTemplate, params object?[]? propertyValues)
    {
        Log.Warning(messageTemplate, propertyValues);
    }

    public void LogWarning(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Log.Warning(exception, messageTemplate, propertyValues);
    }

    public void LogError(string message, Exception ex)
    {
        Log.Error(ex, message);
    }

    public void LogError(string messageTemplate, params object?[]? propertyValues)
    {
        Log.Error(messageTemplate, propertyValues);
    }

    public void LogError(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Log.Error(exception, messageTemplate, propertyValues);
    }
}
