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
public class UpdateSongTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new UpdateSongHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object,
            _validationServiceMock.Object);

        _user = new User
        {
            Id = Guid.NewGuid(),
            UploadedSongs = [],
            Username = "user1",
            Email = "user1@mail.com",
            ArtistName = "Artist",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };
        _existingSong = new Song
        {
            Id = Guid.NewGuid(),
            Title = "Original Title",
            Duration = TimeSpan.FromMinutes(3),
            Url = "http://example.com/song.mp3",
            OwnerId = _user.Id,
            Owner = _user
        };
        _user.UploadedSongs.Add(_existingSong);

        _songDto = new SongDto
        {
            Id = _existingSong.Id,
            Title = "Updated Title",
            Duration = TimeSpan.FromMinutes(4),
            Url = "http://example.com/updated_song.mp3",
            OwnerId = _user.Id,
            GenreId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid()
        };

        _mapperMock.Setup(m => m.Map(_songDto, _existingSong)).Verifiable();
        _mapperMock.Setup(m => m.Map<SongDto>(_existingSong)).Returns(_songDto);

        _unitOfWorkMock.Setup(u =>
                u.SongRepository.GetByOwnerIdAndTitleWhereIdNotEqual(_songDto.OwnerId, _songDto.Title, _songDto.Id,
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        _unitOfWorkMock.Setup(u => u.UserRepository.GetById(_user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_user);
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.SongRepository, _songDto.Id,
                It.IsAny<CancellationToken>(), LogAction.Update))
            .ReturnsAsync(_existingSong);
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserRepository, _songDto.OwnerId,
                It.IsAny<CancellationToken>(), LogAction.Get))
            .ReturnsAsync(_user);
        _validationServiceMock.Setup(v =>
                v.AssertNavigationEntityExists<Song, Genre>(_unitOfWorkMock.Object.GenreRepository, _songDto.GenreId,
                    It.IsAny<CancellationToken>(), LogAction.Update))
            .Returns(Task.CompletedTask);
        _validationServiceMock.Setup(v =>
                v.AssertNavigationEntityExists<Song, Album>(_unitOfWorkMock.Object.AlbumRepository, _songDto.AlbumId,
                    It.IsAny<CancellationToken>(), LogAction.Update))
            .Returns(Task.CompletedTask);
        _validationServiceMock.Setup(v =>
                v.EnsureUserUniquePropertyExceptId(
                    It.IsAny<Func<Guid, string, Guid, CancellationToken, Task<IEnumerable<Song>>>>(), _songDto.OwnerId,
                    nameof(_songDto.Title), _songDto.Title, _songDto.Id, CancellationToken.None, LogAction.Update))
            .Returns(Task.CompletedTask);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private UpdateSongHandler _handler;
    private Song _existingSong;
    private SongDto _songDto;
    private User _user;

    [Test]
    public async Task Handle_ValidUpdate_UpdatesSongSuccessfully()
    {
        // Arrange
        var updateSongCommand = new UpdateSong(_songDto);

        // Act
        var result = await _handler.Handle(updateSongCommand, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), _existingSong.Id, LogAction.Update), Times.Once());
        Assert.That(result.Title, Is.EqualTo(_songDto.Title));
    }

    [Test]
    public void Handle_SongTitleNotUnique_ThrowsDuplicateException()
    {
        // Arrange
        var updateSongCommand = new UpdateSong(_songDto);

        var duplicateSong = new Song
        {
            Id = Guid.NewGuid(),
            Title = _songDto.Title,
            OwnerId = _songDto.OwnerId,
            Url = "http://example.com/duplicate_song.mp3"
        };
        _unitOfWorkMock.Setup(u =>
                u.SongRepository.GetByOwnerIdAndTitleWhereIdNotEqual(_songDto.OwnerId, _songDto.Title, _songDto.Id,
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync([duplicateSong]);

        var exception = new DuplicateException("A song with the same title already exists.");

        _validationServiceMock.Setup(v =>
                v.EnsureUserUniquePropertyExceptId(
                    It.IsAny<Func<Guid, string, Guid, CancellationToken, Task<IEnumerable<Song>>>>(), _songDto.OwnerId,
                    nameof(_songDto.Title), _songDto.Title, _songDto.Id, CancellationToken.None, LogAction.Update))
            .ThrowsAsync(exception);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(updateSongCommand, CancellationToken.None));
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), It.IsAny<Guid>(), LogAction.Update), Times.Never());
    }

    [Test]
    public void Handle_OwnerDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var notFoundException = new NotFoundException("Owner not found.");
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserRepository, _songDto.OwnerId,
                It.IsAny<CancellationToken>(), It.IsAny<LogAction>()))
            .ThrowsAsync(notFoundException);

        var updateSongCommand = new UpdateSong(_songDto);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(updateSongCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), It.IsAny<Guid>(), LogAction.Update), Times.Never());
    }

    [Test]
    public void Handle_SongNotOwnedByUser_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var updateSongCommand = new UpdateSong(_songDto);
        _user.UploadedSongs = [];

        var unauthorizedException = new UnauthorizedAccessException("User does not own Song");
        _loggerMock.Setup(l =>
                l.LogAndThrowNotAuthorizedException(nameof(Song), _songDto.Id, nameof(User), _songDto.OwnerId,
                    LogAction.Update))
            .Throws(unauthorizedException);

        // Act & Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => _handler.Handle(updateSongCommand, CancellationToken.None));
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), It.IsAny<Guid>(), LogAction.Update), Times.Never());
    }
}
