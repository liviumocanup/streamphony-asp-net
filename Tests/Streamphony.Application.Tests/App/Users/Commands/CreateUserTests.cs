using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Users.Commands;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Users.Commands;

[TestFixture]
public class CreateUserTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private CreateUserHandler _handler;
    private UserCreationDto _userCreationDto;
    private User _userEntity;
    private UserDto _userDto;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _handler = new CreateUserHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);

        _userCreationDto = new UserCreationDto
        {
            Username = "newuser",
            Email = "newuser@example.com",
            ArtistName = "New Artist",
            DateOfBirth = new DateOnly(1990, 1, 1),
            ProfilePictureUrl = ""
        };

        _userEntity = new User
        {
            Id = Guid.NewGuid(),
            Username = _userCreationDto.Username,
            Email = _userCreationDto.Email,
            ArtistName = _userCreationDto.ArtistName,
            DateOfBirth = _userCreationDto.DateOfBirth,
            ProfilePictureUrl = ""
        };

        _userDto = new UserDto
        {
            Id = _userEntity.Id,
            Username = _userEntity.Username,
            Email = _userEntity.Email,
            ArtistName = _userEntity.ArtistName,
            DateOfBirth = _userEntity.DateOfBirth,
            ProfilePictureUrl = ""
        };

        _mapperMock.Setup(m => m.Map<User>(_userCreationDto)).Returns(_userEntity);
        _mapperMock.Setup(m => m.Map<UserDto>(_userEntity)).Returns(_userDto);

        _unitOfWorkMock.Setup(u => u.UserRepository.GetByUsernameOrEmail(_userCreationDto.Username, _userCreationDto.Email, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((User)null!);
        _unitOfWorkMock.Setup(u => u.UserRepository.Add(_userEntity, It.IsAny<CancellationToken>())).ReturnsAsync(_userEntity);
    }

    [Test]
    public async Task Handle_UserIsValid_CreatesUserSuccessfully()
    {
        // Arrange
        var createUserCommand = new CreateUser(_userCreationDto);

        // Act
        var result = await _handler.Handle(createUserCommand, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), _userEntity.Id, LogAction.Create), Times.Once());
        Assert.That(result.Id, Is.EqualTo(_userDto.Id));
    }

    [Test]
    public void Handle_UsernameAlreadyExists_ThrowsDuplicateException()
    {
        // Arrange
        var createUserCommand = new CreateUser(_userCreationDto);

        var existingUser = _userEntity;
        existingUser.Email = "different@example.com";
        _unitOfWorkMock.Setup(u => u.UserRepository.GetByUsernameOrEmail(_userCreationDto.Username, _userCreationDto.Email, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(existingUser);

        var duplicateException = new DuplicateException("User already exists.");
        _loggerMock.Setup(l => l.LogAndThrowDuplicateException(nameof(User), nameof(_userCreationDto.Username), _userCreationDto.Username, LogAction.Create))
                   .Throws(duplicateException);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(createUserCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), It.IsAny<Guid>(), LogAction.Create), Times.Never());
    }

    [Test]
    public void Handle_EmailAlreadyExists_ThrowsDuplicateException()
    {
        // Arrange
        var createUserCommand = new CreateUser(_userCreationDto);

        var existingUser = _userEntity;
        existingUser.Username = "different";
        _unitOfWorkMock.Setup(u => u.UserRepository.GetByUsernameOrEmail(_userCreationDto.Username, _userCreationDto.Email, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(existingUser);

        var duplicateException = new DuplicateException("User already exists.");
        _loggerMock.Setup(l => l.LogAndThrowDuplicateException(nameof(User), nameof(_userCreationDto.Email), _userCreationDto.Email, LogAction.Create))
                        .Throws(duplicateException);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(createUserCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), It.IsAny<Guid>(), LogAction.Create), Times.Never());
    }
}
