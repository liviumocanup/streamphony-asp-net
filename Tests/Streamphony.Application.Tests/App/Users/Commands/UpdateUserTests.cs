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
public class UpdateUserTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private UpdateUserHandler _handler;
    private UserDto _userDto;
    private User _existingUser;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new UpdateUserHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _userDto = new UserDto
        {
            Id = Guid.NewGuid(),
            Username = "updatedUser",
            Email = "updatedUser@example.com",
            ArtistName = "Updated Artist",
            DateOfBirth = new DateOnly(1991, 1, 1)
        };

        _existingUser = new User
        {
            Id = _userDto.Id,
            Username = "originalUser",
            Email = "originalUser@example.com",
            ArtistName = "Original Artist",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        _mapperMock.Setup(m => m.Map(_userDto, _existingUser)).Verifiable();
        _mapperMock.Setup(m => m.Map<UserDto>(_existingUser)).Returns(_userDto);

        _unitOfWorkMock.Setup(u => u.UserRepository.GetByUsernameOrEmailWhereIdNotEqual(_userDto.Username, _userDto.Email, _userDto.Id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync((User)null!);
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserRepository, _userDto.Id, It.IsAny<CancellationToken>(), LogAction.Update))
                                .ReturnsAsync(_existingUser);
    }

    [Test]
    public async Task Handle_ValidUpdate_UpdatesUserSuccessfully()
    {
        // Arrange
        var updateUserCommand = new UpdateUser(_userDto);

        // Act
        var result = await _handler.Handle(updateUserCommand, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), _existingUser.Id, LogAction.Update), Times.Once());
        _mapperMock.Verify(m => m.Map(_userDto, _existingUser), Times.Once());
        Assert.That(result.Username, Is.EqualTo(_userDto.Username));
    }

    [Test]
    public void Handle_UsernameAlreadyExists_ThrowsDuplicateException()
    {
        // Arrange
        var updateUserCommand = new UpdateUser(_userDto);

        var conflictingUser = _existingUser;
        conflictingUser.Id = Guid.NewGuid();
        conflictingUser.Username = _userDto.Username;
        _unitOfWorkMock.Setup(u => u.UserRepository.GetByUsernameOrEmailWhereIdNotEqual(_userDto.Username, _userDto.Email, _userDto.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(conflictingUser);

        var duplicateException = new DuplicateException("User with username already exists.");
        _loggerMock.Setup(l => l.LogAndThrowDuplicateException(nameof(User), nameof(_userDto.Username), _userDto.Username, LogAction.Update))
                    .Throws(duplicateException);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(updateUserCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), It.IsAny<Guid>(), LogAction.Update), Times.Never());
    }

    [Test]
    public void Handle_EmailAlreadyExists_ThrowsDuplicateException()
    {
        // Arrange
        var updateUserCommand = new UpdateUser(_userDto);

        var conflictingUser = _existingUser;
        conflictingUser.Id = Guid.NewGuid();
        conflictingUser.Email = _userDto.Email;
        _unitOfWorkMock.Setup(u => u.UserRepository.GetByUsernameOrEmailWhereIdNotEqual(_userDto.Username, _userDto.Email, _userDto.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(conflictingUser);

        var duplicateException = new DuplicateException("User with email already exists.");
        _loggerMock.Setup(l => l.LogAndThrowDuplicateException(nameof(User), nameof(_userDto.Email), _userDto.Email, LogAction.Update))
                    .Throws(duplicateException);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(updateUserCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), It.IsAny<Guid>(), LogAction.Update), Times.Never());
    }
}
