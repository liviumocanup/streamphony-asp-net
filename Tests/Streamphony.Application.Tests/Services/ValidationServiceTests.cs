using System.Linq.Expressions;
using Moq;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.Services;

[TestFixture]
public class ValidationServiceTests
{
    [SetUp]
    public void Setup()
    {
        _defaultSong = new Song { Id = Guid.NewGuid(), Title = "Melody", Url = "melody.com" };
        _defaultUser = new User
        {
            Id = Guid.NewGuid(),
            Username = "user1",
            Email = "user1@mail.ru",
            ArtistName = "Artist1",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };
        _defaultAlbum = new Album { Id = Guid.NewGuid(), Title = "Revolver", OwnerId = _defaultUser.Id };
        _defaultGenre = new Genre { Id = Guid.NewGuid(), Name = "Pop", Description = "Pop music" };

        _validationService = new ValidationService(_loggingServiceMock.Object);

        // Reset mocks
        _songRepositoryMock.Reset();
        _albumRepositoryMock.Reset();
        _userRepositoryMock.Reset();
        _loggingServiceMock.Reset();

        // Setup mocks with default behaviors
        _songRepositoryMock.Setup(repo => repo.GetById(_defaultSong.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_defaultSong);
        _albumRepositoryMock.Setup(repo => repo.GetById(_defaultAlbum.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_defaultAlbum);
        _userRepositoryMock.Setup(repo => repo.GetById(_defaultUser.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_defaultUser);
    }

    private Song _defaultSong;
    private Album _defaultAlbum;
    private User _defaultUser;
    private Genre _defaultGenre;

    private readonly Mock<ILoggingService> _loggingServiceMock = new();
    private readonly Mock<IRepository<Song>> _songRepositoryMock = new();
    private readonly Mock<IRepository<Album>> _albumRepositoryMock = new();
    private readonly Mock<IRepository<User>> _userRepositoryMock = new();
    private ValidationService _validationService;
    private readonly CancellationToken _cancellationToken = new();

    [Test]
    public async Task GetExistingEntity_EntityExists_ReturnsEntity()
    {
        // Arrange
        var songId = _defaultSong.Id;

        // Act
        var result =
            await _validationService.GetExistingEntity(_songRepositoryMock.Object, songId, _cancellationToken,
                LogAction.Get);

        // Assert
        Assert.That(result, Is.EqualTo(_defaultSong));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowNotFoundException(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<LogAction>()),
            Times.Never());
    }

    [Test]
    public void GetExistingEntity_EntityDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var entityId = _defaultSong.Id;
        _songRepositoryMock.Setup(repo => repo.GetById(entityId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Song)null!);
        _loggingServiceMock.Setup(log =>
                log.LogAndThrowNotFoundException(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<LogAction>()))
            .Throws(new NotFoundException($"Song with Id '{entityId}' not found."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() =>
            _validationService.GetExistingEntity(_songRepositoryMock.Object, entityId, _cancellationToken,
                LogAction.Get));

        Assert.That(ex.Message, Is.EqualTo($"Song with Id '{entityId}' not found."));
        _loggingServiceMock.Verify(log => log.LogAndThrowNotFoundException("Song", entityId, LogAction.Get),
            Times.Once());
    }

    [Test]
    public async Task GetExistingEntityWithInclude_EntityExists_ReturnsEntity()
    {
        // Arrange
        var genre = _defaultGenre;

        Task<Genre?> getGenreWithSongs(Guid id, CancellationToken token, Expression<Func<Genre, object>>[] includes)
        {
            return Task.FromResult<Genre?>(genre);
        }

        // Act
        var result = await _validationService.GetExistingEntityWithInclude<Genre>(
            getGenreWithSongs,
            genre.Id,
            LogAction.Get,
            _cancellationToken,
            g => g.Songs
        );

        // Assert
        Assert.That(result, Is.EqualTo(genre));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowNotFoundException(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<LogAction>()),
            Times.Never());
    }

    [Test]
    public void GetExistingEntityWithInclude_EntityDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var entityId = _defaultGenre.Id;

        static Task<Genre?> getGenreWithSongs(Guid id, CancellationToken token,
            Expression<Func<Genre, object>>[] includes)
        {
            return Task.FromResult<Genre?>(null);
        }

        _loggingServiceMock.Setup(log => log.LogAndThrowNotFoundException("Genre", entityId, LogAction.Get))
            .Throws(new NotFoundException($"Genre with Id '{entityId}' not found."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() =>
            _validationService.GetExistingEntityWithInclude<Genre>(
                getGenreWithSongs,
                entityId,
                LogAction.Get,
                _cancellationToken,
                g => g.Songs
            ));

        Assert.That(ex.Message, Is.EqualTo($"Genre with Id '{entityId}' not found."));
        _loggingServiceMock.Verify(log => log.LogAndThrowNotFoundException("Genre", entityId, LogAction.Get),
            Times.Once());
    }

    [Test]
    public void AssertEntityExists_EntityExists_DoesNotThrow()
    {
        // Arrange
        var albumId = _defaultAlbum.Id;

        // Act & Assert
        Assert.DoesNotThrowAsync(() =>
            _validationService.AssertEntityExists(_albumRepositoryMock.Object, albumId, _cancellationToken));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowNotFoundException(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<LogAction>()),
            Times.Never());
    }

    [Test]
    public void AssertEntityExists_EntityDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var entityId = _defaultAlbum.Id;
        _albumRepositoryMock.Setup(repo => repo.GetById(entityId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Album)null!);
        _loggingServiceMock.Setup(log => log.LogAndThrowNotFoundException("Album", entityId, LogAction.Delete))
            .Throws(new NotFoundException($"Album with Id '{entityId}' not found."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() =>
            _validationService.AssertEntityExists(_albumRepositoryMock.Object, entityId, _cancellationToken,
                LogAction.Delete));

        Assert.That(ex.Message, Is.EqualTo($"Album with Id '{entityId}' not found."));
        _loggingServiceMock.Verify(log => log.LogAndThrowNotFoundException("Album", entityId, LogAction.Delete),
            Times.Once());
    }

    [Test]
    public void AssertNavigationEntityExists_NavigationEntityExists_DoesNotThrow()
    {
        // Arrange
        var userId = _defaultUser.Id;

        // Act & Assert
        Assert.DoesNotThrowAsync(() =>
            _validationService.AssertNavigationEntityExists<Album, User>(_userRepositoryMock.Object, userId,
                _cancellationToken));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowNotFoundExceptionForNavigation(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<Guid>(), It.IsAny<LogAction>()), Times.Never());
    }

    [Test]
    public void AssertNavigationEntityExists_NavigationEntityDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var navId = _defaultUser.Id;
        _userRepositoryMock.Setup(repo => repo.GetById(navId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null!);
        _loggingServiceMock.Setup(log =>
                log.LogAndThrowNotFoundExceptionForNavigation("Album", "User", navId, LogAction.Create))
            .Throws(new NotFoundException($"User with Id '{navId}' not found."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() =>
            _validationService.AssertNavigationEntityExists<Album, User>(_userRepositoryMock.Object, navId,
                _cancellationToken, LogAction.Create));

        Assert.That(ex.Message, Is.EqualTo($"User with Id '{navId}' not found."));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowNotFoundExceptionForNavigation("Album", "User", navId, LogAction.Create),
            Times.Once());
    }

    [Test]
    public void AssertNavigationEntityExists_NullId_DoesNotThrow()
    {
        // Act & Assert
        Assert.DoesNotThrowAsync(() =>
            _validationService.AssertNavigationEntityExists<Song, User>(_userRepositoryMock.Object, null,
                _cancellationToken));

        _loggingServiceMock.Verify(
            log => log.LogAndThrowNotFoundExceptionForNavigation(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<Guid>(), It.IsAny<LogAction>()), Times.Never());
    }

    [Test]
    public void AssertNavigationEntityExists_ValidId_ForwardsCorrectly()
    {
        // Arrange
        Guid? navId = _defaultUser.Id;
        _userRepositoryMock.Setup(repo => repo.GetById(navId.Value, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_defaultUser);

        // Act & Assert
        Assert.DoesNotThrowAsync(() =>
            _validationService.AssertNavigationEntityExists<Song, User>(_userRepositoryMock.Object, navId,
                _cancellationToken, LogAction.Create)
        );
        _userRepositoryMock.Verify(repo => repo.GetById(navId.Value, It.IsAny<CancellationToken>()), Times.Once());
        _loggingServiceMock.Verify(
            log => log.LogAndThrowNotFoundExceptionForNavigation(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<Guid>(), It.IsAny<LogAction>()), Times.Never());
    }

    [Test]
    public void EnsureUniqueProperty_DuplicateEntity_ThrowsDuplicateException()
    {
        // Arrange
        var genreName = nameof(Genre.Name);

        Task<Genre?> getByNameFunc(string name, CancellationToken token)
        {
            return Task.FromResult<Genre?>(_defaultGenre);
        }

        _loggingServiceMock.Setup(log =>
                log.LogAndThrowDuplicateException("Genre", nameof(Genre.Name), genreName, LogAction.Create))
            .Throws(new DuplicateException($"Genre with Name '{genreName}' already exists."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<DuplicateException>(() =>
            _validationService.EnsureUniqueProperty(getByNameFunc, nameof(Genre.Name), genreName, _cancellationToken,
                LogAction.Create));

        Assert.That(ex.Message, Is.EqualTo($"Genre with Name '{genreName}' already exists."));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowDuplicateException("Genre", nameof(Genre.Name), genreName, LogAction.Create),
            Times.Once());
    }

    [Test]
    public void EnsureUniqueProperty_NoDuplicates_DoesNotThrow()
    {
        // Arrange
        static Task<Genre?> getByNameFunc(string name, CancellationToken token)
        {
            return Task.FromResult<Genre?>(null);
        }

        var genreName = _defaultGenre.Name;

        // Act & Assert
        Assert.DoesNotThrowAsync(() => _validationService.EnsureUniqueProperty(getByNameFunc, nameof(Genre.Name),
            genreName, _cancellationToken, LogAction.Create));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowDuplicateException(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(),
                It.IsAny<LogAction>()), Times.Never());
    }

    [Test]
    public void EnsureUniquePropertyExceptId_DuplicatesExist_ThrowsDuplicateException()
    {
        // Arrange
        var entityId = new Guid();
        var propertyName = nameof(_defaultGenre.Name);
        var propertyValue = _defaultGenre.Name;
        var existingGenres = new List<Genre> { _defaultGenre };

        Task<IEnumerable<Genre>> getEntitiesFunc(string name, Guid id, CancellationToken token)
        {
            return Task.FromResult<IEnumerable<Genre>>(existingGenres);
        }

        _loggingServiceMock.Setup(log =>
                log.LogAndThrowDuplicateException("Genre", propertyName, propertyValue, LogAction.Update))
            .Throws(new DuplicateException($"Genre with {propertyName} '{propertyValue}' already exists."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<DuplicateException>(() =>
            _validationService.EnsureUniquePropertyExceptId(getEntitiesFunc, propertyName, propertyValue, entityId,
                _cancellationToken, LogAction.Update)
        );

        Assert.That(ex.Message, Is.EqualTo($"Genre with {propertyName} '{propertyValue}' already exists."));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowDuplicateException("Genre", propertyName, propertyValue, LogAction.Update),
            Times.Once());
    }

    [Test]
    public void EnsureUniquePropertyExceptId_NoDuplicates_DoesNotThrow()
    {
        // Arrange
        var entityId = _defaultGenre.Id;
        var propertyName = nameof(_defaultGenre.Name);
        var propertyValue = _defaultGenre.Name;

        static Task<IEnumerable<Genre>> getEntitiesFunc(string name, Guid id, CancellationToken token)
        {
            return Task.FromResult<IEnumerable<Genre>>([]);
        }

        // Act & Assert
        Assert.DoesNotThrowAsync(() =>
            _validationService.EnsureUniquePropertyExceptId(getEntitiesFunc, propertyName, propertyValue, entityId,
                _cancellationToken, LogAction.Update)
        );
        _loggingServiceMock.Verify(
            log => log.LogAndThrowDuplicateException(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(),
                It.IsAny<LogAction>()), Times.Never());
    }

    [Test]
    public void EnsureUserUniqueProperty_DuplicateExists_ThrowsDuplicateException()
    {
        // Arrange
        var ownerId = new Guid();
        var propertyName = nameof(_defaultAlbum.Title);
        var propertyValue = _defaultAlbum.Title;

        Task<Album?> getAlbumByOwnerAndTitleFunc(Guid userId, string title, CancellationToken token)
        {
            return Task.FromResult<Album?>(_defaultAlbum);
        }

        _loggingServiceMock.Setup(log =>
                log.LogAndThrowDuplicateExceptionForUser("Album", propertyName, propertyValue, ownerId,
                    LogAction.Create))
            .Throws(new DuplicateException(
                $"Album with {propertyName} '{propertyValue}' already exists for User with Id '{ownerId}'."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<DuplicateException>(() =>
            _validationService.EnsureUserUniqueProperty(getAlbumByOwnerAndTitleFunc, ownerId, propertyName,
                propertyValue, _cancellationToken, LogAction.Create)
        );

        Assert.That(ex.Message,
            Is.EqualTo($"Album with {propertyName} '{propertyValue}' already exists for User with Id '{ownerId}'."));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowDuplicateExceptionForUser("Album", propertyName, propertyValue, ownerId,
                LogAction.Create), Times.Once());
    }

    [Test]
    public void EnsureUserUniqueProperty_NoDuplicates_DoesNotThrow()
    {
        // Arrange
        var ownerId = _defaultAlbum.OwnerId;
        var propertyName = nameof(_defaultAlbum.Title);
        var propertyValue = _defaultAlbum.Title;

        Func<Guid, string, CancellationToken, Task<Album?>> getAlbumByOwnerAndTitleFunc = (userId, title, token) =>
            Task.FromResult<Album?>(null);

        // Act & Assert
        Assert.DoesNotThrowAsync(() =>
            _validationService.EnsureUserUniqueProperty(getAlbumByOwnerAndTitleFunc, ownerId, propertyName,
                propertyValue, _cancellationToken, LogAction.Create)
        );
        _loggingServiceMock.Verify(
            log => log.LogAndThrowDuplicateExceptionForUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(),
                It.IsAny<Guid>(), It.IsAny<LogAction>()), Times.Never());
    }

    [Test]
    public void EnsureUserUniquePropertyExceptId_DuplicatesExist_ThrowsDuplicateException()
    {
        // Arrange
        var entityId = _defaultAlbum.Id;
        var ownerId = _defaultAlbum.OwnerId;
        var propertyName = nameof(_defaultAlbum.Title);
        var propertyValue = _defaultAlbum.Title;
        var existingAlbums = new List<Album> { _defaultAlbum };

        Task<IEnumerable<Album>> getAlbumsFunc(Guid userId, string title, Guid exId, CancellationToken token)
        {
            return Task.FromResult<IEnumerable<Album>>(existingAlbums);
        }

        _loggingServiceMock.Setup(log =>
                log.LogAndThrowDuplicateExceptionForUser("Album", propertyName, propertyValue, ownerId,
                    LogAction.Update))
            .Throws(new DuplicateException(
                $"Album with {propertyName} '{propertyValue}' already exists for User with Id '{ownerId}'."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<DuplicateException>(() =>
            _validationService.EnsureUserUniquePropertyExceptId(getAlbumsFunc, ownerId, propertyName, propertyValue,
                entityId, _cancellationToken, LogAction.Update)
        );

        Assert.That(ex.Message,
            Is.EqualTo($"Album with {propertyName} '{propertyValue}' already exists for User with Id '{ownerId}'."));
        _loggingServiceMock.Verify(
            log => log.LogAndThrowDuplicateExceptionForUser("Album", propertyName, propertyValue, ownerId,
                LogAction.Update), Times.Once());
    }

    [Test]
    public void EnsureUserUniquePropertyExceptId_NoDuplicates_DoesNotThrow()
    {
        // Arrange
        var entityId = _defaultAlbum.Id;
        var ownerId = _defaultAlbum.OwnerId;
        var propertyName = nameof(_defaultAlbum.Title);
        var propertyValue = _defaultAlbum.Title;

        static Task<IEnumerable<Album>> getAlbumsFunc(Guid userId, string title, Guid exId, CancellationToken token)
        {
            return Task.FromResult<IEnumerable<Album>>([]);
        }

        // Act & Assert
        Assert.DoesNotThrowAsync(() =>
            _validationService.EnsureUserUniquePropertyExceptId(getAlbumsFunc, ownerId, propertyName, propertyValue,
                entityId, _cancellationToken, LogAction.Update)
        );
        _loggingServiceMock.Verify(
            log => log.LogAndThrowDuplicateExceptionForUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(),
                It.IsAny<Guid>(), It.IsAny<LogAction>()), Times.Never());
    }
}
