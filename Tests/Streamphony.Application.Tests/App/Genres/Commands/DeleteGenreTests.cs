using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Commands;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Genres.Commands;

[TestFixture]
public class DeleteGenreTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new DeleteGenreHandler(_unitOfWorkMock.Object, _loggerMock.Object, _validationServiceMock.Object);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private DeleteGenreHandler _handler;

    [Test]
    public async Task Handle_GenreExists_DeletesGenreSuccessfully()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        var deleteGenreCommand = new DeleteGenre(genreId);

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.GenreRepository, genreId,
                It.IsAny<CancellationToken>(), LogAction.Delete))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.GenreRepository.Delete(genreId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(deleteGenreCommand, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _unitOfWorkMock.Verify(u => u.GenreRepository.Delete(genreId, CancellationToken.None), Times.Once());
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), genreId, LogAction.Delete), Times.Once());
    }

    [Test]
    public void Handle_GenreDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        var deleteGenreCommand = new DeleteGenre(genreId);
        var exception = new NotFoundException("Genre not found.");

        _validationServiceMock.Setup(v => v.AssertEntityExists(_unitOfWorkMock.Object.GenreRepository, genreId,
                It.IsAny<CancellationToken>(), LogAction.Delete))
            .ThrowsAsync(exception);

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(
            () => _handler.Handle(deleteGenreCommand, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Genre not found."));
        _unitOfWorkMock.Verify(u => u.GenreRepository.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), genreId, LogAction.Delete), Times.Never());
    }
}
