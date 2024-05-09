using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.UserPreferences.Queries;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.UserPreferences.Queries;

[TestFixture]
public class GetUserPreferenceByIdTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private GetUserPreferenceByIdHandler _handler;
    private User _userEntity;
    private UserPreference _userPreference;
    private UserPreferenceDto _userPreferenceDto;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new GetUserPreferenceByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _userEntity = new User
        {
            Id = Guid.NewGuid(),
            Username = "user",
            ArtistName = "Artist",
            Email = "user@mail.com",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        _userPreference = new UserPreference
        {
            Id = _userEntity.Id,
            DarkMode = true,
            Language = "en",
            User = _userEntity
        };

        _userPreferenceDto = new UserPreferenceDto
        {
            Id = _userPreference.Id,
            DarkMode = _userPreference.DarkMode,
            Language = _userPreference.Language
        };

        _unitOfWorkMock.Setup(u => u.UserPreferenceRepository.GetById(_userPreference.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(_userPreference);
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserPreferenceRepository, _userPreference.Id, It.IsAny<CancellationToken>(), It.IsAny<LogAction>()))
                              .ReturnsAsync(_userPreference);
        _mapperMock.Setup(m => m.Map<UserPreferenceDto>(_userPreference)).Returns(_userPreferenceDto);
    }

    [Test]
    public async Task Handle_ValidId_ReturnsUserPreferenceDto()
    {
        // Arrange
        var getUserPreferenceByIdQuery = new GetUserPreferenceById(_userPreference.Id);

        // Act
        var result = await _handler.Handle(getUserPreferenceByIdQuery, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(_userPreferenceDto));
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), _userPreference.Id, LogAction.Get), Times.Once());
    }

    [Test]
    public void Handle_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var newId = Guid.NewGuid();
        var getUserPreferenceByIdQuery = new GetUserPreferenceById(newId);
        var notFoundException = new NotFoundException("User preference not found.");

        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserPreferenceRepository, newId, It.IsAny<CancellationToken>(), It.IsAny<LogAction>()))
                              .ThrowsAsync(notFoundException);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(getUserPreferenceByIdQuery, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), newId, LogAction.Get), Times.Never());
    }
}
