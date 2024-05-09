using System.Linq.Expressions;
using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Users.Queries;

[TestFixture]
public class GetUserByIdTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private GetUserByIdHandler _handler;
    private User _existingUser;
    private UserDetailsDto _userDetailsDto;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new GetUserByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _existingUser = new User
        {
            Id = Guid.NewGuid(),
            Username = "user1",
            Email = "user1@example.com",
            ArtistName = "Artist1",
            DateOfBirth = new DateOnly(1985, 5, 15),
            ProfilePictureUrl = "",
            UploadedSongs = [],
            OwnedAlbums = [],
            Preferences = new UserPreference()
        };

        _userDetailsDto = new UserDetailsDto
        {
            Id = _existingUser.Id,
            Username = _existingUser.Username,
            Email = _existingUser.Email,
            ArtistName = _existingUser.ArtistName,
            DateOfBirth = _existingUser.DateOfBirth,
            UploadedSongs = [],
            OwnedAlbums = [],
            Preferences = new UserPreferenceDto()
        };

        _unitOfWorkMock.Setup(u => u.UserRepository.GetByIdWithInclude(_existingUser.Id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(_existingUser);

        _validationServiceMock.Setup(v => v.GetExistingEntityWithInclude<User>(
            _unitOfWorkMock.Object.UserRepository.GetByIdWithInclude,
            _existingUser.Id,
            LogAction.Get,
            It.IsAny<CancellationToken>(),
            user => user.UploadedSongs,
            user => user.Preferences,
            user => user.OwnedAlbums))
            .ReturnsAsync(_existingUser);

        _mapperMock.Setup(m => m.Map<UserDetailsDto>(_existingUser)).Returns(_userDetailsDto);
    }

    [Test]
    public async Task Handle_ValidId_ReturnsUserDetailsDto()
    {
        // Arrange
        var userId = _existingUser.Id;
        var getUserByIdQuery = new GetUserById(userId);

        // Act
        var result = await _handler.Handle(getUserByIdQuery, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(_userDetailsDto));
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), userId, LogAction.Get), Times.Once());
    }

    [Test]
    public void Handle_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var invalidUserId = Guid.NewGuid();
        var getUserByIdQuery = new GetUserById(invalidUserId);
        var notFoundException = new NotFoundException("User not found.");

        _unitOfWorkMock.Setup(u => u.UserRepository.GetByIdWithInclude(invalidUserId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync((User)null!);

        _validationServiceMock.Setup(v => v.GetExistingEntityWithInclude<User>(
            _unitOfWorkMock.Object.UserRepository.GetByIdWithInclude,
            invalidUserId,
            LogAction.Get,
            It.IsAny<CancellationToken>(),
            user => user.UploadedSongs,
            user => user.Preferences,
            user => user.OwnedAlbums))
            .ThrowsAsync(notFoundException);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(getUserByIdQuery, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), invalidUserId, LogAction.Get), Times.Never());
    }
}
