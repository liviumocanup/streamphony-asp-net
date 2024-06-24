using Moq;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.Services;

[TestFixture]
public class LoggingServiceTests
{
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILoggingProvider>();
        _loggingService = new LoggingService(_loggerMock.Object);
    }

    private Mock<ILoggingProvider> _loggerMock;
    private LoggingService _loggingService;

    [Test]
    public void LogSuccess_WithEntityNameAndAction_CallsLogInformationWithCorrectParameters()
    {
        // Arrange
        var entityName = nameof(Song);
        LogAction action = LogAction.Get;

        // Act
        _loggingService.LogSuccess(entityName, action);

        // Assert
        _loggerMock.Verify(m => m.LogInformation(
            It.Is<string>(s => s == "{LogAction} success for all {EntityType}s."),
            It.Is<object[]>(o => o[0].Equals(action) && o[1].Equals(entityName))
        ), Times.Once);
    }

    [Test]
    public void LogSuccess_WithEntityNameEntityIdAndAction_CallsLogInformationWithCorrectParameters()
    {
        // Arrange
        var entityName = nameof(Album);
        var entityId = Guid.NewGuid();
        LogAction action = LogAction.Create;

        // Act
        _loggingService.LogSuccess(entityName, entityId, action);

        // Assert
        _loggerMock.Verify(m => m.LogInformation(
            It.Is<string>(s => s == "{LogAction} success for {EntityType} with Id '{EntityId}'."),
            It.Is<object[]>(o => o[0].Equals(action) && o[1].Equals(entityName) && o[2].Equals(entityId))
        ), Times.Once);
    }

    [Test]
    public void LogAndThrowNotAuthorizedException_LogsWarningAndThrowsUnauthorizedException()
    {
        // Arrange
        var entityName = nameof(Song);
        var entityId = Guid.NewGuid();
        var navName = nameof(User);
        var navId = Guid.NewGuid();
        LogAction action = LogAction.Update;

        // Act & Assert
        var ex = Assert.Throws<UnauthorizedException>(() =>
            _loggingService.LogAndThrowNotAuthorizedException(entityName, entityId, navName, navId, action));

        _loggerMock.Verify(m => m.LogWarning(
            It.Is<string>(s =>
                s ==
                "{LogAction} attempt for non-owned {EntityType} with Id '{EntityId}' by {NavigationType} with Id '{NavigationId}'."),
            It.Is<object[]>(o =>
                o[0].Equals(action) && o[1].Equals(entityName) && o[2].Equals(entityId) && o[3].Equals(navName) &&
                o[4].Equals(navId))
        ), Times.Once);
        Assert.That(ex.Message,
            Is.EqualTo($"{navName} with Id '{navId}' does not own {entityName} with Id '{entityId}'."));
    }

    [Test]
    public void LogAndThrowNotFoundException_LogsWarningAndThrowsNotFoundException()
    {
        // Arrange
        var entityName = nameof(Genre);
        var entityId = Guid.NewGuid();
        LogAction action = LogAction.Get;

        // Act & Assert
        var ex = Assert.Throws<NotFoundException>(() =>
            _loggingService.LogAndThrowNotFoundException(entityName, entityId, action));

        _loggerMock.Verify(m => m.LogWarning(
            It.Is<string>(s => s == "{LogAction} attempt for non-existing {EntityType} with Id '{EntityId}'."),
            It.Is<object[]>(o => o[0].Equals(action) && o[1].Equals(entityName) && o[2].Equals(entityId))
        ), Times.Once);
        Assert.That(ex.Message, Is.EqualTo($"{entityName} with Id '{entityId}' not found."));
    }

    [Test]
    public void LogAndThrowNotFoundExceptionForNavigation_LogsWarningAndThrowsNotFoundException()
    {
        // Arrange
        var entityName = nameof(Song);
        var navName = nameof(Album);
        var navId = Guid.NewGuid();
        LogAction action = LogAction.Create;

        // Act & Assert
        var ex = Assert.Throws<NotFoundException>(() =>
            _loggingService.LogAndThrowNotFoundExceptionForNavigation(entityName, navName, navId, action));

        _loggerMock.Verify(m => m.LogWarning(
            It.Is<string>(s =>
                s ==
                "{LogAction} attempt for {EntityType} with non-existing {NavigationType} with Id '{NavigationId}'."),
            It.Is<object[]>(o =>
                o[0].Equals(action) && o[1].Equals(entityName) && o[2].Equals(navName) && o[3].Equals(navId))
        ), Times.Once);
        Assert.That(ex.Message, Is.EqualTo($"{navName} with Id '{navId}' not found."));
    }

    [Test]
    public void LogAndThrowDuplicateException_LogsWarningAndThrowsDuplicateException()
    {
        // Arrange
        var entityName = nameof(User);
        var propertyName = "Email";
        var propertyValue = "ufenik@mail.com";
        LogAction action = LogAction.Create;

        // Act & Assert
        var ex = Assert.Throws<DuplicateException>(() =>
            _loggingService.LogAndThrowDuplicateException(entityName, propertyName, propertyValue, action));

        _loggerMock.Verify(m => m.LogWarning(
            It.Is<string>(s =>
                s == "{LogAction} attempt for {EntityType} with existing {DuplicateProperty} '{PropertyValue}'."),
            It.Is<object[]>(o =>
                o[0].Equals(action) && o[1].Equals(entityName) && o[2].Equals(propertyName) &&
                o[3].Equals(propertyValue))
        ), Times.Once);
        Assert.That(ex.Message, Is.EqualTo($"{entityName} with {propertyName} '{propertyValue}' already exists."));
    }

    [Test]
    public void LogAndThrowDuplicateExceptionForUser_LogsWarningAndThrowsDuplicateException()
    {
        // Arrange
        var entityName = nameof(Album);
        var propertyName = "Title";
        var propertyValue = "Abbey Road";
        var ownerId = Guid.NewGuid();
        LogAction action = LogAction.Update;

        // Act & Assert
        var ex = Assert.Throws<DuplicateException>(() =>
            _loggingService.LogAndThrowDuplicateExceptionForUser(entityName, propertyName, propertyValue, ownerId,
                action));

        _loggerMock.Verify(m => m.LogWarning(
            It.Is<string>(s =>
                s ==
                "{LogAction} attempt for {EntityType} with existing {DuplicateProperty} '{PropertyValue}' for {NavigationType} with Id '{NavigationId}'."),
            It.Is<object[]>(o =>
                o[0].Equals(action) && o[1].Equals(entityName) && o[2].Equals(propertyName) &&
                o[3].Equals(propertyValue) && o[4].Equals(nameof(User)) && o[5].Equals(ownerId))
        ), Times.Once);
        Assert.That(ex.Message,
            Is.EqualTo(
                $"{entityName} with {propertyName} '{propertyValue}' already exists for User with Id '{ownerId}'."));
    }
}
