using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.WebAPI.Tests.Factories;
using Streamphony.WebAPI.Tests.Helpers;

namespace Streamphony.WebAPI.Tests.Controllers;

[TestFixture]
public class SongControllerTests
{
    [SetUp]
    public void SetUp()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        Utilities.InitializeDbForTests(db);
    }

    [TearDown]
    public void TearDown()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureDeleted();

        _client.Dispose();
        _factory.Dispose();
    }

    private CustomWebApplicationFactory _factory;
    private HttpClient _client;

    [Test]
    public async Task CreateSong_Success_ReturnsCreated()
    {
        // Arrange
        var newSongDto = new SongCreationDto
        {
            Title = "New Song",
            Url = "https://newtest.com",
            Duration = new TimeSpan(0, 1, 1),
            OwnerId = Utilities.UserId1
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/songs", newSongDto);
        var createdSong = await response.Content.ReadFromJsonAsync<SongDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(createdSong!.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(createdSong.Title, Is.EqualTo(newSongDto.Title));
            Assert.That(createdSong.Url, Is.EqualTo(newSongDto.Url));
        });
    }

    [Test]
    public async Task CreateSong_EmptyTitle_ReturnsBadRequest()
    {
        // Arrange
        var invalidSongDto = new SongCreationDto
        {
            Title = "",
            Url = "https://test.com",
            Duration = new TimeSpan(0, 1, 1),
            OwnerId = Utilities.UserId1
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/songs", invalidSongDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("'Title' must not be empty."));
        });
    }


    [Test]
    public async Task CreateSong_DuplicateTitle_ReturnsConflict()
    {
        // Arrange
        var conflictingSongDto = new SongCreationDto
        {
            Title = Utilities.DbSong1.Title,
            Duration = new TimeSpan(0, 1, 1),
            Url = "https://test.com",
            OwnerId = Utilities.DbSong1.OwnerId
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/songs", conflictingSongDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content,
                Does.Contain(
                    $"Song with Title 'TestSong' already exists for User with Id '{Utilities.DbSong1.OwnerId}'."));
        });
    }

    [Test]
    public async Task CreateSong_OwnerDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var nonExistentOwnerId = Guid.NewGuid();
        var songDto = new SongCreationDto
        {
            Title = "New Song",
            Duration = new TimeSpan(0, 1, 1),
            Url = "https://test.com",
            OwnerId = nonExistentOwnerId
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/songs", songDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"User with Id '{nonExistentOwnerId}' not found."));
        });
    }

    [Test]
    public async Task UpdateSong_Success_ReturnsOk()
    {
        // Arrange
        var updatedSongDto = new SongDto
        {
            Id = Utilities.SongId1,
            Title = "UpdatedSongTest",
            Duration = new TimeSpan(0, 1, 1),
            OwnerId = Utilities.UserId1,
            AlbumId = Utilities.AlbumId1,
            GenreId = Utilities.GenreId1,
            Url = "https://updatedtest.com"
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/songs", updatedSongDto);
        var updatedSong = await response.Content.ReadFromJsonAsync<SongDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(updatedSong!.Id, Is.EqualTo(updatedSongDto.Id));
            Assert.That(updatedSong.Title, Is.EqualTo(updatedSongDto.Title));
            Assert.That(updatedSong.Url, Is.EqualTo(updatedSongDto.Url));
        });
    }

    [Test]
    public async Task UpdateSong_EmptyTitle_ReturnsBadRequest()
    {
        // Arrange
        var invalidSongDto = new SongDto
        {
            Id = Utilities.SongId1,
            Title = "",
            Duration = new TimeSpan(0, 1, 1),
            OwnerId = Utilities.UserId1,
            AlbumId = Utilities.AlbumId1,
            GenreId = Utilities.GenreId1,
            Url = "https://test.com"
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/songs", invalidSongDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("'Title' must not be empty."));
        });
    }

    [Test]
    public async Task UpdateSong_SongNotInDb_ReturnsNotFound()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var nonExistingSongDto = new SongDto
        {
            Id = songId,
            Title = "Non-Existing Album",
            OwnerId = Utilities.UserId1,
            Url = "https://test.com",
            Duration = new TimeSpan(0, 1, 1)
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/songs", nonExistingSongDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Song with Id '{songId}' not found."));
        });
    }

    [Test]
    public async Task UpdateSong_UserAlreadyHasSongWithDuplicateTitle_ReturnsConflict()
    {
        // Arrange
        var conflictingUpdateSongDto = new SongDto
        {
            Id = Utilities.SongId1,
            Title = "UpdatedSong",
            OwnerId = Utilities.UserId1,
            Url = "https://test.com",
            Duration = new TimeSpan(0, 1, 1)
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/songs", conflictingUpdateSongDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content,
                Does.Contain($"Song with Title 'UpdatedSong' already exists for User with Id '{Utilities.UserId1}'."));
        });
    }

    [Test]
    public async Task UpdateSong_UserDoesNotOwnSong_ReturnsUnauthorized()
    {
        // Arrange
        var nonOwnerUpdateSongDto = new SongDto
        {
            Id = Utilities.SongId1,
            Title = "UpdatedSong",
            OwnerId = Utilities.UserId2,
            Url = "https://test.com",
            Duration = new TimeSpan(0, 1, 1)
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/songs", nonOwnerUpdateSongDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(content,
                Does.Contain($"User with Id '{Utilities.UserId2}' does not own Song with Id '{Utilities.SongId1}'."));
        });
    }

    [Test]
    public async Task GetAllSongs_ReturnsAllSongs()
    {
        // Act
        var response = await _client.GetAsync("api/songs");
        var songs = await response.Content.ReadFromJsonAsync<IEnumerable<SongDto>>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(songs, Is.Not.Empty);
            Assert.That(songs!.Count(), Is.EqualTo(2));
        });
    }

    [Test]
    public async Task GetSongById_Success_ReturnsSong()
    {
        // Arrange
        var songId = Utilities.SongId1;

        // Act
        var response = await _client.GetAsync($"api/songs/{songId}");
        var song = await response.Content.ReadFromJsonAsync<SongDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(song, Is.Not.Null);
            Assert.That(song!.Id, Is.EqualTo(songId));
        });
    }

    [Test]
    public async Task GetSongById_SongNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"api/songs/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Song with Id '{nonExistingId}' not found."));
        });
    }

    [Test]
    public async Task DeleteSong_Success_ReturnsNoContent()
    {
        // Arrange
        var songId = Utilities.SongId1;

        // Act
        var deleteResponse = await _client.DeleteAsync($"api/songs/{songId}");
        var fetchResponse = await _client.GetAsync($"api/songs/{songId}");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(fetchResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        });
    }

    [Test]
    public async Task DeleteSong_SongNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"api/songs/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Song with Id '{nonExistingId}' not found."));
        });
    }
}
