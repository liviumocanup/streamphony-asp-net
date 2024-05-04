using System.Runtime.CompilerServices;
using Streamphony.Application.Abstractions.Logging;
using System.IO.Abstractions;

namespace Streamphony.Infrastructure.Logging
{
    public class FileLoggingService : ILoggingService, ILoggerManager
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _logDirectoryPath;
        private readonly IDateTimeProvider _dateTimeProvider;

        public FileLoggingService(IDateTimeProvider dateTimeProvider, IFileSystem fileSystem)
        {
            _dateTimeProvider = dateTimeProvider;
            _fileSystem = fileSystem;
            _logDirectoryPath = Path.Combine(_fileSystem.Directory.GetCurrentDirectory(), "Logs");

            if (!_fileSystem.Directory.Exists(_logDirectoryPath))
            {
                _fileSystem.Directory.CreateDirectory(_logDirectoryPath);
            }
        }

        public async Task LogAsync(string message, [CallerMemberName] string methodName = "")
        {
            var currentDateTime = _dateTimeProvider.Now;
            var fileName = $"Logs_{currentDateTime:yyyy-MM-dd}.txt";
            var filePath = Path.Combine(_logDirectoryPath, fileName);

            var logMessage = $"{currentDateTime:yyyy-MM-dd HH:mm:ss} - {methodName} called: {message}{Environment.NewLine}";

            await _fileSystem.File.AppendAllTextAsync(filePath, logMessage);
        }

        public async Task LogAsync(string message, Exception ex, [CallerMemberName] string methodName = "")
        {
            var currentDateTime = _dateTimeProvider.Now;
            var fileName = $"Logs_{currentDateTime:yyyy-MM-dd}.txt";
            var filePath = Path.Combine(_logDirectoryPath, fileName);

            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            var logMessage = $"{currentDateTime:yyyy-MM-dd HH:mm:ss} - {methodName} called: {message}{errorMessage}{Environment.NewLine}";

            await _fileSystem.File.AppendAllTextAsync(filePath, logMessage);
        }

        public void LogError(string message, Exception ex)
        {
            LogAsync(message, ex).Wait();
        }

        public void LogError(string messageTemplate, params object?[]? propertyValues)
        {
            LogAsync(messageTemplate).Wait();
        }

        public void LogError(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            LogAsync(messageTemplate, exception!).Wait();
        }

        public void LogInformation(string message)
        {
            LogAsync(message).Wait();
        }

        public void LogInformation(string messageTemplate, params object?[]? propertyValues)
        {
            LogAsync(messageTemplate).Wait();
        }

        public void LogInformation(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            LogAsync(messageTemplate, exception!).Wait();
        }

        public void LogWarning(string message)
        {
            LogAsync(message).Wait();
        }

        public void LogWarning(string messageTemplate, params object?[]? propertyValues)
        {
            LogAsync(messageTemplate).Wait();
        }

        public void LogWarning(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            LogAsync(messageTemplate, exception!).Wait();
        }
    }
}
