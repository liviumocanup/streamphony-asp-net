using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.WebAPI.Tests.Factories;
using Streamphony.WebAPI.Tests.Helpers;

namespace Streamphony.WebAPI.Tests.Controllers;

[TestFixture]
public class UserControllerTests
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
    public async Task CreateUser_Success_ReturnsCreated()
    {
        // Arrange
        var userDto = new UserCreationDto
        {
            Username = "NewUser",
            ArtistName = "NewArtist",
            Email = "new@mail.com",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/users", userDto);
        var createdUser = await response.Content.ReadFromJsonAsync<UserDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(createdUser!.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(createdUser.Username, Is.EqualTo(userDto.Username));
        });
    }

    [Test]
    public async Task CreateUser_EmptyUsername_ReturnsBadRequest()
    {
        // Arrange
        var invalidUserDto = new UserCreationDto
        {
            Username = "",
            ArtistName = "NewArtist",
            Email = "new@mail.com",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/users", invalidUserDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("'Username' must not be empty."));
        });
    }

    [Test]
    public async Task CreateUser_DuplicateEmail_ReturnsConflict()
    {
        // Arrange
        var conflictingUserDto = new UserCreationDto
        {
            Username = "newuser",
            ArtistName = "NewArtist",
            Email = Utilities.DbUser1.Email,
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/users", conflictingUserDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content, Does.Contain($"User with Email '{conflictingUserDto.Email}' already exists."));
        });
    }

    [Test]
    public async Task UpdateUser_Success_ReturnsOk()
    {
        // Arrange
        var updateUserDto = new UserDto
        {
            Id = Utilities.UserId1,
            Username = "updateduser",
            ArtistName = "NewArtist",
            Email = "new@mail.com",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/users", updateUserDto);
        var updatedUser = await response.Content.ReadFromJsonAsync<UserDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(updatedUser!.Id, Is.EqualTo(updateUserDto.Id));
            Assert.That(updateUserDto.Username, Is.EqualTo(updatedUser.Username));
        });
    }

    [Test]
    public async Task UpdateUser_EmptyEmail_ReturnsBadRequest()
    {
        // Arrange
        var invalidUserDto = new UserDto
        {
            Username = "",
            ArtistName = "NewArtist",
            Email = "",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/users", invalidUserDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Does.Contain("'Email' must not be empty."));
        });
    }

    [Test]
    public async Task UpdateUser_UserNotInDb_ReturnsNotFound()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        var nonExistingUserDto = new UserDto
        {
            Id = userId,
            Username = "updateduser",
            ArtistName = "NewArtist",
            Email = "new@mail.com",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/users", nonExistingUserDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"User with Id '{userId}' not found."));
        });
    }

    [Test]
    public async Task UpdateUser_AnotherUserAlreadyHasUsername_ReturnsConflict()
    {
        // Arrange
        Guid userId = Utilities.UserId1;
        var conflictingUpdateUserDto = new UserDto
        {
            Id = userId,
            Username = Utilities.DbUser2.Username,
            ArtistName = "NewArtist",
            Email = "new@mail.com",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        // Act
        var response = await _client.PutAsJsonAsync("api/users", conflictingUpdateUserDto);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            Assert.That(content, Does.Contain($"User with Username '{conflictingUpdateUserDto.Username}' already exists."));
        });
    }

    [Test]
    public async Task GetAllUsers_ReturnsAllUsers()
    {
        // Act
        var response = await _client.GetAsync("api/users");
        var users = await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(users, Is.Not.Empty);
            Assert.That(users!.Count(), Is.EqualTo(2));
            Assert.That(users!.Any(u => u.Email == Utilities.DbUser1.Email), Is.True);
        });
    }

    [Test]
    public async Task GetUserById_Success_ReturnsUser()
    {
        // Arrange
        var userId = Utilities.UserId1;

        // Act
        var response = await _client.GetAsync($"api/users/{userId}");
        var user = await response.Content.ReadFromJsonAsync<UserDto>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(user, Is.Not.Null);
            Assert.That(user!.Id, Is.EqualTo(userId));
            Assert.That(user.Username, Is.EqualTo(Utilities.DbUser1.Username));
        });
    }

    [Test]
    public async Task GetUserById_UserNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"api/users/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"User with Id '{nonExistingId}' not found."));
        });
    }

    [Test]
    public async Task DeleteUser_Success_ReturnsNoContent()
    {
        // Arrange
        var userId = Utilities.UserId1;

        // Act
        var response = await _client.DeleteAsync($"api/users/{userId}");
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var dbUser = db.Users.Find(userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(dbUser, Is.Null);
        });
    }

    [Test]
    public async Task DeleteUser_UserNotInDb_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"api/users/{nonExistingId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(content, Does.Contain($"User with Id '{nonExistingId}' not found."));
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