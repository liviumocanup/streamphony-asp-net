using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.WebAPI.Tests.Factories;
using Streamphony.WebAPI.Tests.Helpers;

namespace Streamphony.WebAPI.Tests.Controllers;

[TestFixture]
public class AlbumControllerTests
{
    private CustomWebApplicationFactory _factory;
    private HttpClient _client;

    [SetUp]
    public void SetUp()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        Utilities.InitializeDbForTests(db);
    }

    [Test]
    public async Task CreateAlbum_Success_ReturnsCreated()
    {
        // Arrange
        var albumDto = new AlbumCreationDto { Title = "New Album", OwnerId = Utilities.UserId1 };

        // Act
        var response = await _client.PostAsJsonAsync("api/albums", albumDto);
        var createdAlbum = await response.Content.ReadFromJsonAsync<AlbumDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(createdAlbum, Is.Not.Null);
            Assert.That(createdAlbum!.Title, Is.EqualTo(albumDto.Title));
        });
    }

    [Test]
    public async Task CreateAlbum_TitleIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        var albumDto = new AlbumCreationDto { Title = "" };

        // Act
        var response = await _client.PostAsJsonAsync("api/albums", albumDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("'Title' must not be empty."));
        });
    }

    [Test]
    public async Task CreateAlbum_OwnerDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        Guid ownerGuid = Guid.NewGuid();
        var albumDto = new AlbumCreationDto { Title = "New Album", OwnerId = ownerGuid };

        // Act
        var response = await _client.PostAsJsonAsync("api/albums", albumDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"User with Id '{ownerGuid}' not found."));
        });
    }

    [Test]
    public async Task CreateAlbum_AlbumAlreadyExists_ReturnsConflict()
    {
        // Arrange
        var dbAlbum = Utilities.DbAlbum1;
        var albumDto = new AlbumCreationDto { Title = dbAlbum.Title, OwnerId = dbAlbum.OwnerId };

        // Act
        var response = await _client.PostAsJsonAsync("api/albums", albumDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content, Does.Contain($"Album with Title '{albumDto.Title}' already exists for User with Id '{albumDto.OwnerId}'."));
        });
    }

    [Test]
    public async Task UpdateAlbum_Success_ReturnsOk()
    {
        // Arrange
        var updateAlbumDto = new AlbumDto { Id = Utilities.AlbumId1, Title = "Updated Album", OwnerId = Utilities.UserId1 };

        // Act
        var response = await _client.PutAsJsonAsync("api/albums", updateAlbumDto);
        var updatedAlbum = await response.Content.ReadFromJsonAsync<AlbumDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(updatedAlbum!.Title, Is.EqualTo(updateAlbumDto.Title));
        });
    }

    [Test]
    public async Task UpdateAlbum_TitleIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        var invalidUpdateAlbumDto = new AlbumDto { Id = Utilities.AlbumId1, Title = "", OwnerId = Utilities.UserId1 };

        // Act
        var response = await _client.PutAsJsonAsync("api/albums", invalidUpdateAlbumDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("'Title' must not be empty."));
        });
    }

    [Test]
    public async Task UpdateAlbum_AlbumNotInDb_ReturnsNotFound()
    {
        // Arrange
        Guid albumId = Guid.NewGuid();
        var nonExistingAlbumDto = new AlbumDto { Id = albumId, Title = "Non-Existing Album", OwnerId = Utilities.UserId1 };

        // Act
        var response = await _client.PutAsJsonAsync("api/albums", nonExistingAlbumDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Album with Id '{albumId}' not found."));
        });
    }

    [Test]
    public async Task UpdateAlbum_UserAlreadyHasAlbumWithDuplicateTitle_ReturnsConflict()
    {
        // Arrange
        var conflictingUpdateAlbumDto = new AlbumDto { Id = Utilities.AlbumId1, Title = "UpdatedAlbum", OwnerId = Utilities.UserId1, };

        // Act
        var response = await _client.PutAsJsonAsync("api/albums", conflictingUpdateAlbumDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content, Does.Contain($"Album with Title 'UpdatedAlbum' already exists for User with Id '{Utilities.UserId1}'."));
        });
    }

    [Test]
    public async Task GetAllAlbums_ReturnsAllAlbums()
    {
        // Act
        var response = await _client.GetAsync("api/albums");
        var albums = await response.Content.ReadFromJsonAsync<IEnumerable<AlbumDto>>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(albums, Is.Not.Empty);
            Assert.That(albums!.Count(), Is.EqualTo(2));
            Assert.That(albums!.Any(a => a.Title == Utilities.DbAlbum1.Title), Is.True);
        });
    }

    [Test]
    public async Task GetAlbumById_Success_ReturnsAlbum()
    {
        // Act
        var response = await _client.GetAsync($"api/albums/{Utilities.AlbumId1}");
        var album = await response.Content.ReadFromJsonAsync<AlbumDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(album, Is.Not.Null);
            Assert.That(album!.Id, Is.EqualTo(Utilities.AlbumId1));
        });
    }

    [Test]
    public async Task GetAlbumById_AlbumNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"api/albums/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Album with Id '{nonExistingId}' not found."));
        });
    }

    [Test]
    public async Task DeleteAlbum_Success_ReturnsNoContent()
    {
        // Arrange
        var albumId = Utilities.AlbumId1;

        // Act
        var response = await _client.DeleteAsync($"api/albums/{albumId}");
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var dbAlbum = db.Albums.Find(albumId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(dbAlbum, Is.Null);
        });
    }

    [Test]
    public async Task DeleteAlbum_AlbumNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"api/albums/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Album with Id '{nonExistingId}' not found."));
        });
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
}
