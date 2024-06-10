using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.Commands;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Songs.Commands;

[TestFixture]
public class DeleteSongTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new DeleteSongHandler(_unitOfWorkMock.Object, _loggerMock.Object, _validationServiceMock.Object);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private DeleteSongHandler _handler;

    [Test]
    public async Task Handle_SongExists_DeletesSongSuccessfully()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var deleteSongCommand = new DeleteSong(songId);

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.SongRepository, songId,
                It.IsAny<CancellationToken>(), LogAction.Delete))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SongRepository.Delete(songId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(deleteSongCommand, CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
        _unitOfWorkMock.Verify(u => u.SongRepository.Delete(songId, CancellationToken.None), Times.Once());
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), songId, LogAction.Delete), Times.Once());
    }

    [Test]
    public void Handle_SongDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var deleteSongCommand = new DeleteSong(songId);
        var exception = new NotFoundException("Song not found.");

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.SongRepository, songId,
                It.IsAny<CancellationToken>(), LogAction.Delete))
            .ThrowsAsync(exception);

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(deleteSongCommand, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Song not found."));
        _unitOfWorkMock.Verify(u => u.SongRepository.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), songId, LogAction.Delete), Times.Never());
    }
}
