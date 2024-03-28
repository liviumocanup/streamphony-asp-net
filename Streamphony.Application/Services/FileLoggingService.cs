namespace Streamphony.Application.Services
{
    using Streamphony.Application.Interfaces;

    public class FileLoggingService : ILoggingService
    {
        private readonly string _logDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        private readonly IDateTimeProvider _dateTimeProvider;

        public FileLoggingService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            if (!Directory.Exists(_logDirectoryPath))
            {
                Directory.CreateDirectory(_logDirectoryPath);
            }
        }

        public async Task LogAsync(string message)
        {
            var currentDateTime = _dateTimeProvider.Now;
            var fileName = $"Logs_{currentDateTime:yyyy-MM-dd}.txt";
            var filePath = Path.Combine(_logDirectoryPath, fileName);

            var logMessage = $"{currentDateTime:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";

            await File.AppendAllTextAsync(filePath, logMessage);
        }
    }

}