using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.Commands;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Songs.Commands;

[TestFixture]
public class CreateSongTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new CreateSongHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object,
            _validationServiceMock.Object);

        _songCreationDto = new SongCreationDto
        {
            Title = "New Song",
            Duration = TimeSpan.FromMinutes(4),
            Url = "http://example.com/new_song.mp3",
            OwnerId = Guid.NewGuid(),
            GenreId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid()
        };

        _songEntity = new Song
        {
            Title = _songCreationDto.Title,
            Duration = _songCreationDto.Duration,
            Url = _songCreationDto.Url,
            OwnerId = _songCreationDto.OwnerId,
            GenreId = _songCreationDto.GenreId,
            AlbumId = _songCreationDto.AlbumId
        };

        _songDto = new SongDto
        {
            Id = Guid.NewGuid(),
            Title = _songEntity.Title,
            Duration = _songEntity.Duration,
            Url = _songEntity.Url,
            OwnerId = _songEntity.OwnerId,
            GenreId = _songEntity.GenreId,
            AlbumId = _songEntity.AlbumId
        };

        _mapperMock.Setup(m => m.Map<Song>(_songCreationDto)).Returns(_songEntity);
        _unitOfWorkMock.Setup(u => u.SongRepository.Add(_songEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_songEntity);
        _mapperMock.Setup(m => m.Map<SongDto>(_songEntity)).Returns(_songDto);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private CreateSongHandler _handler;
    private SongCreationDto _songCreationDto;
    private Song _songEntity;
    private SongDto _songDto;

    [Test]
    public async Task Handle_SongIsValid_CreatesSongSuccessfully()
    {
        // Arrange
        var createSongCommand = new CreateSong(_songCreationDto);

        // Act
        var result = await _handler.Handle(createSongCommand, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), _songEntity.Id, LogAction.Create), Times.Once());
        Assert.That(result.Id, Is.EqualTo(_songDto.Id));
    }

    [Test]
    public void Handle_SongTitleNotUnique_ThrowsDuplicateException()
    {
        // Arrange
        var duplicateException = new DuplicateException("A song with the same title already exists.");
        _validationServiceMock.Setup(v =>
                v.EnsureUserUniqueProperty(It.IsAny<Func<Guid, string, CancellationToken, Task<Song?>>>(),
                    _songCreationDto.OwnerId, nameof(_songCreationDto.Title), _songCreationDto.Title,
                    CancellationToken.None, LogAction.Create))
            .ThrowsAsync(duplicateException);

        var createSongCommand = new CreateSong(_songCreationDto);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(createSongCommand, CancellationToken.None));
        _unitOfWorkMock.Verify(u => u.SongRepository.Add(It.IsAny<Song>(), It.IsAny<CancellationToken>()),
            Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), It.IsAny<Guid>(), LogAction.Create), Times.Never());
    }

    // Add tests for NotFoundException when Owner, Genre, or Album IDs are invalid or not found
    [Test]
    public void Handle_OwnerDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var notFoundException = new NotFoundException("Owner not found.");
        _validationServiceMock.Setup(v =>
                v.AssertNavigationEntityExists<Song, User>(_unitOfWorkMock.Object.UserRepository,
                    _songCreationDto.OwnerId, CancellationToken.None, LogAction.Create))
            .ThrowsAsync(notFoundException);

        var createSongCommand = new CreateSong(_songCreationDto);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(createSongCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), It.IsAny<Guid>(), LogAction.Create), Times.Never());
    }
}
