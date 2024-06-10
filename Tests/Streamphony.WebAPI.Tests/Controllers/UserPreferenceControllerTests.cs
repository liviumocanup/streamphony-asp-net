using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.WebAPI.Tests.Factories;
using Streamphony.WebAPI.Tests.Helpers;

namespace Streamphony.WebAPI.Tests.Controllers;

[TestFixture]
public class UserPreferenceControllerTests
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
    public async Task CreateUserPreference_Success_ReturnsCreated()
    {
        // Arrange
        var newUserPreferenceDto = new UserPreferenceDto { Id = Utilities.UserId1, Language = "en" };

        // Act
        var response = await _client.PostAsJsonAsync("api/userPreferences", newUserPreferenceDto);
        var createdPreference = await response.Content.ReadFromJsonAsync<UserPreferenceDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(createdPreference, Is.Not.Null);
            Assert.That(createdPreference!.Language, Is.EqualTo("en"));
        });
    }

    [Test]
    public async Task CreateUserPreference_LanguageTooLong_ReturnsBadRequest()
    {
        // Arrange
        var userPreferenceDto = new UserPreferenceDto { Id = Utilities.UserId1, Language = "english" };

        // Act
        var response = await _client.PostAsJsonAsync("api/userPreferences", userPreferenceDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("The length of 'Language' must be 2 characters or fewer."));
        });
    }

    [Test]
    public async Task CreateUserPreference_OwnerDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var ownerGuid = Guid.NewGuid();
        var userPreferenceDto = new UserPreferenceDto { Id = ownerGuid, Language = "en" };

        // Act
        var response = await _client.PostAsJsonAsync("api/userPreferences", userPreferenceDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"User with Id '{ownerGuid}' not found."));
        });
    }

    [Test]
    public async Task CreateUserPreference_UserPreferenceAlreadyExists_ReturnsConflict()
    {
        // Arrange
        var userPreferenceDto = new UserPreferenceDto { Id = Utilities.DbUserPreference.Id, Language = "en" };

        // Act
        var response = await _client.PostAsJsonAsync("api/userPreferences", userPreferenceDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content, Does.Contain($"UserPreference with Id '{userPreferenceDto.Id}' already exists."));
        });
    }


    [Test]
    public async Task UpdateUserPreference_Success_ReturnsOk()
    {
        // Arrange
        var updateUserPreferenceDto = new UserPreferenceDto { Id = Utilities.DbUserPreference.Id, Language = "fr" };

        // Act
        var response = await _client.PutAsJsonAsync("api/userPreferences", updateUserPreferenceDto);
        var updatedUserPreference = await response.Content.ReadFromJsonAsync<UserPreferenceDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(updatedUserPreference!.Language, Is.EqualTo(updateUserPreferenceDto.Language));
        });
    }

    [Test]
    public async Task UpdateUserPreference_LanguageTooLong_ReturnsBadRequest()
    {
        // Arrange
        var invalidUpdateUserPreferenceDto = new UserPreferenceDto
            { Id = Utilities.DbUserPreference.Id, Language = "French" };

        // Act
        var response = await _client.PutAsJsonAsync("api/userPreferences", invalidUpdateUserPreferenceDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("The length of 'Language' must be 2 characters or fewer."));
        });
    }

    [Test]
    public async Task UpdateUserPreference_UserPreferenceNotInDb_ReturnsNotFound()
    {
        // Arrange
        var userPreferenceId = Guid.NewGuid();
        var nonExistingUserPreferenceDto = new UserPreferenceDto { Id = userPreferenceId };

        // Act
        var response = await _client.PutAsJsonAsync("api/userPreferences", nonExistingUserPreferenceDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"UserPreference with Id '{userPreferenceId}' not found."));
        });
    }

    [Test]
    public async Task GetAllUserPreferences_ReturnsAllUserPreferences()
    {
        // Act
        var response = await _client.GetAsync("api/userPreferences");
        var userPreferences = await response.Content.ReadFromJsonAsync<IEnumerable<UserPreferenceDto>>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(userPreferences, Is.Not.Empty);
            Assert.That(userPreferences!.Count(), Is.EqualTo(1));
        });
    }

    [Test]
    public async Task GetUserPreferenceById_Success_ReturnsUserPreference()
    {
        // Act
        var response = await _client.GetAsync($"api/userPreferences/{Utilities.DbUserPreference.Id}");
        var userPreference = await response.Content.ReadFromJsonAsync<UserPreferenceDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(userPreference, Is.Not.Null);
            Assert.That(userPreference!.Id, Is.EqualTo(Utilities.DbUserPreference.Id));
        });
    }

    [Test]
    public async Task GetUserPreferenceById_UserPreferenceNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"api/userPreferences/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"UserPreference with Id '{nonExistingId}' not found."));
        });
    }

    [Test]
    public async Task DeleteUserPreference_Success_ReturnsNoContent()
    {
        // Arrange
        var userPreferenceId = Utilities.DbUserPreference.Id;

        // Act
        var response = await _client.DeleteAsync($"api/userPreferences/{userPreferenceId}");
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var dbUserPreference = db.UserPreferences.Find(userPreferenceId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(dbUserPreference, Is.Null);
        });
    }

    [Test]
    public async Task DeleteUserPreference_UserPreferenceNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"api/userPreferences/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"UserPreference with Id '{nonExistingId}' not found."));
        });
    }
}
