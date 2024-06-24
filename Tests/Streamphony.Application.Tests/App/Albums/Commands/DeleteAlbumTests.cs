using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Commands;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Albums.Commands;

[TestFixture]
public class DeleteAlbumTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new DeleteAlbumHandler(_unitOfWorkMock.Object, _loggerMock.Object, _validationServiceMock.Object);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private DeleteAlbumHandler _handler;

    [Test]
    public async Task Handle_AlbumExists_DeletesAlbumAndLogsSuccess()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var deleteAlbumCommand = new DeleteAlbum(albumId);

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.AlbumRepository, albumId,
                new CancellationToken(), LogAction.Delete))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.AlbumRepository.Delete(albumId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(deleteAlbumCommand, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.AlbumRepository.Delete(albumId, CancellationToken.None), Times.Once());
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Album), albumId, LogAction.Delete), Times.Once());
    }

    [Test]
    public void Handle_AlbumDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var deleteAlbumCommand = new DeleteAlbum(albumId);
        var exception = new NotFoundException("Album not found.");

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.AlbumRepository, albumId,
                new CancellationToken(), LogAction.Delete))
            .ThrowsAsync(exception);

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(
            () => _handler.Handle(deleteAlbumCommand, CancellationToken.None));
        Assert.That(ex, Is.EqualTo(exception));
        _unitOfWorkMock.Verify(u => u.AlbumRepository.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never());
    }
}
