using System.Runtime.CompilerServices;
using Streamphony.Application.Interfaces;
using System.IO.Abstractions;

namespace Streamphony.Infrastructure.Logging
{
    public class FileLoggingService : ILoggingService
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
    }
}
