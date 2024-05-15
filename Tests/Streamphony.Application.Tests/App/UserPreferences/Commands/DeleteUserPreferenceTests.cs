using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.UserPreferences.Commands;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.UserPreferences.Commands;

[TestFixture]
public class DeleteUserPreferenceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private DeleteUserPreferenceHandler _handler;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new DeleteUserPreferenceHandler(_unitOfWorkMock.Object, _loggerMock.Object, _validationServiceMock.Object);
    }

    [Test]
    public async Task Handle_UserPreferenceExists_DeletesSuccessfully()
    {
        // Arrange
        var userPreferenceId = Guid.NewGuid();
        var deleteUserPreferenceCommand = new DeleteUserPreference(userPreferenceId);

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.UserPreferenceRepository, userPreferenceId, It.IsAny<CancellationToken>(), LogAction.Delete))
                              .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.UserPreferenceRepository.Delete(userPreferenceId, It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(deleteUserPreferenceCommand, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _unitOfWorkMock.Verify(u => u.UserPreferenceRepository.Delete(userPreferenceId, CancellationToken.None), Times.Once());
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), userPreferenceId, LogAction.Delete), Times.Once());
    }

    [Test]
    public void Handle_UserPreferenceDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var userPreferenceId = Guid.NewGuid();
        var deleteUserPreferenceCommand = new DeleteUserPreference(userPreferenceId);
        var notFoundException = new NotFoundException("User preference not found.");

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.UserPreferenceRepository, userPreferenceId, It.IsAny<CancellationToken>(), LogAction.Delete))
                              .ThrowsAsync(notFoundException);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(deleteUserPreferenceCommand, CancellationToken.None));
        _unitOfWorkMock.Verify(u => u.UserPreferenceRepository.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), userPreferenceId, LogAction.Delete), Times.Never());
    }
}
