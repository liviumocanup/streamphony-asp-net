using Moq;
using System.IO.Abstractions.TestingHelpers;
using Streamphony.Infrastructure.Logging;
using Streamphony.Application.Interfaces;

namespace Streamphony.Infrastructure.Tests.Logging
{
    public class FileLoggingServiceTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        private readonly DateTime _dateTimeNow = new DateTime(2023, 1, 1);
        private readonly MockFileSystem _mockFileSystem;
        private readonly FileLoggingService _fileLoggingService;

        public FileLoggingServiceTests()
        {
            _mockDateTimeProvider.Setup(m => m.Now).Returns(_dateTimeNow);
            _mockFileSystem = new MockFileSystem();
            _fileLoggingService = new FileLoggingService(_mockDateTimeProvider.Object, _mockFileSystem);
        }

        [Fact]
        public async Task LogAsync_CreatesDirectoryIfNotExists()
        {
            string expectedDirectory = Path.Combine(_mockFileSystem.Directory.GetCurrentDirectory(), "Logs");

            await _fileLoggingService.LogAsync("Test directory creation");

            Assert.True(_mockFileSystem.Directory.Exists(expectedDirectory));
        }

        [Fact]
        public async Task LogAsync_CreatesLogFileWithCorrectContent()
        {
            var expectedLogMessage = $"{_dateTimeNow:yyyy-MM-dd HH:mm:ss} - LogAsync called: Test message{Environment.NewLine}";
            var expectedFileName = $"Logs_{_dateTimeNow:yyyy-MM-dd}.txt";
            var expectedFilePath = Path.Combine(_mockFileSystem.Directory.GetCurrentDirectory(), "Logs", expectedFileName);

            await _fileLoggingService.LogAsync("Test message", "LogAsync");
            var actualLogContent = _mockFileSystem.File.ReadAllText(expectedFilePath);

            Assert.Multiple(() =>
            {
                Assert.True(_mockFileSystem.FileExists(expectedFilePath));
                Assert.Equal(expectedLogMessage, actualLogContent);
            });
        }
    }

}