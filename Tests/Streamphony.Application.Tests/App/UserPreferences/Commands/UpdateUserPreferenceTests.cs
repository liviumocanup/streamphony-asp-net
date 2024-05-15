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
public class UpdateUserPreferenceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private UpdateUserPreferenceHandler _handler;
    private User _userEntity;
    private UserPreferenceDto _userPreferenceDto;
    private UserPreference _existingUserPreference;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new UpdateUserPreferenceHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _userEntity = new User
        {
            Id = Guid.NewGuid(),
            Username = "user",
            ArtistName = "Artist",
            Email = "user@mail.com",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        _userPreferenceDto = new UserPreferenceDto
        {
            Id = _userEntity.Id,
            DarkMode = true,
            Language = "en"
        };

        _existingUserPreference = new UserPreference
        {
            Id = _userEntity.Id,
            DarkMode = false,
            Language = "fr",
            User = _userEntity
        };

        _mapperMock.Setup(m => m.Map(_userPreferenceDto, _existingUserPreference)).Verifiable();
        _mapperMock.Setup(m => m.Map<UserPreferenceDto>(_existingUserPreference)).Returns(_userPreferenceDto);

        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserPreferenceRepository, _userPreferenceDto.Id, It.IsAny<CancellationToken>(), LogAction.Update))
                              .ReturnsAsync(_existingUserPreference);
    }

    [Test]
    public async Task Handle_ValidUpdate_UpdatesUserPreferenceSuccessfully()
    {
        // Arrange
        var updateUserPreferenceCommand = new UpdateUserPreference(_userPreferenceDto);

        // Act
        var result = await _handler.Handle(updateUserPreferenceCommand, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), _existingUserPreference.Id, LogAction.Update), Times.Once());
        _mapperMock.Verify(m => m.Map(_userPreferenceDto, _existingUserPreference), Times.Once());
        Assert.Multiple(() =>
        {
            Assert.That(result.DarkMode, Is.EqualTo(_userPreferenceDto.DarkMode));
            Assert.That(result.Language, Is.EqualTo(_userPreferenceDto.Language));
        });
    }

    [Test]
    public void Handle_UserPreferenceDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var newId = Guid.NewGuid();
        var invalidDto = new UserPreferenceDto { Id = newId, DarkMode = true, Language = "en" };
        var updateUserPreferenceCommand = new UpdateUserPreference(invalidDto);
        var notFoundException = new NotFoundException("User preference not found.");

        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserPreferenceRepository, newId, It.IsAny<CancellationToken>(), LogAction.Update))
                              .ThrowsAsync(notFoundException);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(updateUserPreferenceCommand, CancellationToken.None));
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), newId, LogAction.Update), Times.Never());
    }
}
