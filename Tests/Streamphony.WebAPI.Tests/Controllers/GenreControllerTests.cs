using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.WebAPI.Tests.Factories;
using Streamphony.WebAPI.Tests.Helpers;

namespace Streamphony.WebAPI.Tests.Controllers;

[TestFixture]
public class GenreControllerTests
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
    public async Task CreateGenre_Success_ReturnsCreated()
    {
        // Arrange
        var newGenreDto = new GenreCreationDto
        {
            Name = "New Genre",
            Description = "New Description"
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/genres", newGenreDto);
        var createdGenre = await response.Content.ReadFromJsonAsync<GenreDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(createdGenre!.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(createdGenre.Name, Is.EqualTo(newGenreDto.Name));
        });
    }

    [Test]
    public async Task CreateGenre_EmptyName_ReturnsBadRequest()
    {
        // Arrange
        var invalidGenreDto = new GenreCreationDto
        {
            Name = "",
            Description = "Invalid Description"
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/genres", invalidGenreDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("'Name' must not be empty."));
        });
    }

    [Test]
    public async Task CreateGenre_DuplicateName_ReturnsConflict()
    {
        // Arrange
        var conflictingGenreDto = new GenreCreationDto
        {
            Name = "TestGenre",
            Description = "Another Description"
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/genres", conflictingGenreDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content, Does.Contain("Genre with Name 'TestGenre' already exists."));
        });
    }

    [Test]
    public async Task UpdateGenre_Success_ReturnsOk()
    {
        // Arrange
        var updateGenreDto = new GenreDto
        {
            Id = Utilities.GenreId1,
            Name = "UpdatedTestGenre",
            Description = "Updated Description"
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/genres", updateGenreDto);
        var updatedGenre = await response.Content.ReadFromJsonAsync<GenreDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(updatedGenre!.Id, Is.EqualTo(updateGenreDto.Id));
            Assert.That(updatedGenre.Name, Is.EqualTo(updateGenreDto.Name));
        });
    }

    [Test]
    public async Task UpdateGenre_EmptyName_ReturnsBadRequest()
    {
        // Arrange
        var invalidGenreDto = new GenreDto
        {
            Id = Utilities.GenreId1,
            Name = "",
            Description = "Some Description"
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/genres", invalidGenreDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("'Name' must not be empty."));
        });
    }

    [Test]
    public async Task UpdateGenre_GenreNotInDb_ReturnsNotFound()
    {
        // Arrange
        Guid genreId = Guid.NewGuid();
        var nonExistingGenreDto = new GenreDto
        {
            Id = genreId,
            Name = "NonExistent",
            Description = "Non-existent Description"
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/genres", nonExistingGenreDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Genre with Id '{genreId}' not found."));
        });
    }

    [Test]
    public async Task UpdateGenre_AnotherGenreAlreadyHasName_ReturnsConflict()
    {
        // Arrange
        var conflictingUpdateGenreDto = new GenreDto
        {
            Id = Utilities.GenreId1,
            Name = "UpdatedGenre",
            Description = "Conflict Description"
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/genres", conflictingUpdateGenreDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content, Does.Contain("Genre with Name 'UpdatedGenre' already exists."));
        });
    }

    [Test]
    public async Task GetAllGenres_ReturnsAllGenres()
    {
        // Act
        var response = await _client.GetAsync("api/genres");
        var genres = await response.Content.ReadFromJsonAsync<IEnumerable<GenreDto>>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(genres, Is.Not.Empty);
            Assert.That(genres!.Count(), Is.EqualTo(2));
            Assert.That(genres!.Any(g => g.Name == Utilities.DbGenre1.Name), Is.True);
        });
    }

    [Test]
    public async Task GetGenreById_Success_ReturnsGenre()
    {
        // Arrange
        var genreId = Utilities.GenreId1;

        // Act
        var response = await _client.GetAsync($"api/genres/{genreId}");
        var genre = await response.Content.ReadFromJsonAsync<GenreDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(genre, Is.Not.Null);
            Assert.That(genre!.Id, Is.EqualTo(genreId));
            Assert.That(genre.Name, Is.EqualTo(Utilities.DbGenre1.Name));
        });
    }

    [Test]
    public async Task GetGenreById_GenreNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"api/genres/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Genre with Id '{nonExistingId}' not found."));
        });
    }

    [Test]
    public async Task DeleteGenre_Success_ReturnsNoContent()
    {
        // Arrange
        var genreId = Utilities.GenreId1;

        // Act
        var response = await _client.DeleteAsync($"api/genres/{genreId}");
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var dbGenre = db.Genres.Find(genreId);

        // Assert
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(dbGenre, Is.Null);
        });
    }

    [Test]
    public async Task DeleteGenre_GenreNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"api/genres/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"Genre with Id '{nonExistingId}' not found."));
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
