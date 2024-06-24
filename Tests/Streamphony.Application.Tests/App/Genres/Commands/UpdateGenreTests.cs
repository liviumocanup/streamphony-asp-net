using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Commands;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Genres.Commands;

[TestFixture]
public class UpdateGenreTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new UpdateGenreHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object,
            _validationServiceMock.Object);

        _existingGenre = new Genre
        {
            Id = Guid.NewGuid(),
            Name = "Rock",
            Description = "A popular genre of music originating in the 1950s."
        };

        _genreDto = new GenreDto
        {
            Id = _existingGenre.Id,
            Name = "Updated Rock",
            Description = "Updated description."
        };

        _mapperMock.Setup(m => m.Map(_genreDto, _existingGenre)).Verifiable();
        _mapperMock.Setup(m => m.Map<GenreDto>(_existingGenre)).Returns(_genreDto);

        _unitOfWorkMock.Setup(u =>
                u.GenreRepository.GetByNameWhereIdNotEqual(_genreDto.Name, _genreDto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        _unitOfWorkMock.Setup(u => u.GenreRepository.GetById(_genreDto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_existingGenre);

        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.GenreRepository, _genreDto.Id,
                It.IsAny<CancellationToken>(), LogAction.Update))
            .ReturnsAsync(_existingGenre);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private UpdateGenreHandler _handler;
    private Genre _existingGenre;
    private GenreDto _genreDto;

    [Test]
    public async Task Handle_ValidUpdate_UpdatesGenreSuccessfully()
    {
        // Arrange
        var updateGenreCommand = new UpdateGenre(_genreDto);

        // Act
        var result = await _handler.Handle(updateGenreCommand, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), _existingGenre.Id, LogAction.Update), Times.Once());
        Assert.That(result.Name, Is.EqualTo(_genreDto.Name));
    }

    [Test]
    public void Handle_DuplicateName_ThrowsDuplicateException()
    {
        // Arrange
        var duplicateGenre = new Genre { Id = Guid.NewGuid(), Name = _genreDto.Name, Description = "Duplicate genre." };
        _unitOfWorkMock.Setup(u =>
                u.GenreRepository.GetByNameWhereIdNotEqual(_genreDto.Name, _genreDto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync([duplicateGenre]);

        var updateGenreCommand = new UpdateGenre(_genreDto);
        var exception = new DuplicateException("A genre with the same name already exists.");

        _validationServiceMock.Setup(v =>
                v.EnsureUniquePropertyExceptId(
                    It.IsAny<Func<string, Guid, CancellationToken, Task<IEnumerable<Genre>>>>(), nameof(_genreDto.Name),
                    _genreDto.Name, _genreDto.Id, CancellationToken.None, LogAction.Update))
            .ThrowsAsync(exception);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(updateGenreCommand, CancellationToken.None));
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), It.IsAny<Guid>(), LogAction.Update), Times.Never());
    }

    [Test]
    public void Handle_GenreDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var notFoundGenreId = Guid.NewGuid();
        _genreDto.Id = notFoundGenreId; // Non-existing ID
        var updateGenreCommand = new UpdateGenre(_genreDto);
        var exception = new NotFoundException("Genre not found.");

        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.GenreRepository, notFoundGenreId,
                It.IsAny<CancellationToken>(), LogAction.Update))
            .ThrowsAsync(exception);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(updateGenreCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), It.IsAny<Guid>(), LogAction.Update), Times.Never());
    }
}
