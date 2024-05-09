using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.UserPreferences.Commands;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.UserPreferences.Commands;

[TestFixture]
public class CreateUserPreferenceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private CreateUserPreferenceHandler _handler;
    private UserPreferenceDto _userPreferenceDto;
    private UserPreference _userPreferenceEntity;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new CreateUserPreferenceHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _userPreferenceDto = new UserPreferenceDto
        {
            Id = Guid.NewGuid(),
            DarkMode = true,
            Language = "en"
        };

        _userPreferenceEntity = new UserPreference
        {
            Id = _userPreferenceDto.Id,
            DarkMode = _userPreferenceDto.DarkMode,
            Language = _userPreferenceDto.Language,
            User = new User { Id = _userPreferenceDto.Id, Username = "user1", ArtistName = "Artist", Email = "user1@mail.com", DateOfBirth = new DateOnly(1999, 10, 10) } // Assuming User ID is same as UserPreference ID for simplicity
        };

        _mapperMock.Setup(m => m.Map<UserPreference>(_userPreferenceDto)).Returns(_userPreferenceEntity);
        _mapperMock.Setup(m => m.Map<UserPreferenceDto>(_userPreferenceEntity)).Returns(_userPreferenceDto);

        _unitOfWorkMock.Setup(u => u.UserPreferenceRepository.GetById(_userPreferenceDto.Id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync((UserPreference)null!);
        _unitOfWorkMock.Setup(u => u.UserPreferenceRepository.Add(_userPreferenceEntity, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(_userPreferenceEntity);

        _validationServiceMock.Setup(v => v.AssertNavigationEntityExists<UserPreference, User>(_unitOfWorkMock.Object.UserRepository, _userPreferenceDto.Id, It.IsAny<CancellationToken>(), LogAction.Create))
                        .Returns(Task.CompletedTask);
    }

    [Test]
    public async Task Handle_UserPreferenceIsValid_CreatesUserPreferenceSuccessfully()
    {
        // Arrange
        var createUserPreferenceCommand = new CreateUserPreference(_userPreferenceDto);

        // Act
        var result = await _handler.Handle(createUserPreferenceCommand, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), _userPreferenceEntity.Id, LogAction.Create), Times.Once());
        Assert.That(result, Is.EqualTo(_userPreferenceDto));
    }

    [Test]
    public void Handle_UserDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var createUserPreferenceCommand = new CreateUserPreference(_userPreferenceDto);
        var notFoundException = new NotFoundException("User not found.");
        _validationServiceMock.Setup(v => v.AssertNavigationEntityExists<UserPreference, User>(_unitOfWorkMock.Object.UserRepository, _userPreferenceDto.Id, It.IsAny<CancellationToken>(), LogAction.Create))
                              .ThrowsAsync(notFoundException);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(createUserPreferenceCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), It.IsAny<Guid>(), LogAction.Create), Times.Never());
    }

    [Test]
    public void Handle_UserPreferenceAlreadyExists_ThrowsDuplicateException()
    {
        // Arrange
        var createUserPreferenceCommand = new CreateUserPreference(_userPreferenceDto);
        var duplicateException = new DuplicateException("User preference already exists.");
        _validationServiceMock.Setup(v => v.EnsureUniqueProperty(It.IsAny<Func<Guid, CancellationToken, Task<UserPreference?>>>(), nameof(_userPreferenceDto.Id), _userPreferenceDto.Id, CancellationToken.None, LogAction.Create))
                              .ThrowsAsync(duplicateException);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(createUserPreferenceCommand, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), It.IsAny<Guid>(), LogAction.Create), Times.Never());
    }
}
