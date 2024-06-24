using System.Linq.Expressions;
using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Users.Commands;

[TestFixture]
public class DeleteUserTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new DeleteUserHandler(_unitOfWorkMock.Object, _loggerMock.Object, _validationServiceMock.Object);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private DeleteUserHandler _handler;

    [Test]
    public async Task Handle_UserExists_DeletesUserAndSongsSuccessfully()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var deleteUserCommand = new DeleteUser(userId);

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.UserRepository, userId,
                It.IsAny<CancellationToken>(), LogAction.Delete))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u =>
                u.SongRepository.DeleteWhere(song => song.OwnerId == userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.UserRepository.Delete(userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(deleteUserCommand, CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
        _unitOfWorkMock.Verify(
            u => u.SongRepository.DeleteWhere(It.IsAny<Expression<Func<Song, bool>>>(), CancellationToken.None),
            Times.Once());
        _unitOfWorkMock.Verify(u => u.UserRepository.Delete(userId, CancellationToken.None), Times.Once());
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), userId, LogAction.Delete), Times.Once());
    }

    [Test]
    public void Handle_UserDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var deleteUserCommand = new DeleteUser(userId);
        var exception = new NotFoundException("User not found.");

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.UserRepository, userId,
                It.IsAny<CancellationToken>(), LogAction.Delete))
            .ThrowsAsync(exception);

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(deleteUserCommand, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("User not found."));
        _unitOfWorkMock.Verify(u => u.UserRepository.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), userId, LogAction.Delete), Times.Never());
    }
}
