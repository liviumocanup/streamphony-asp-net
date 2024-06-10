using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.Tests.App.UserPreferences.Queries;

[TestFixture]
public class GetAllUserPreferencesTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _handler = new GetAllUserPreferencesHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);

        _userPreferences =
        [
            new()
            {
                Id = Guid.NewGuid(),
                DarkMode = true,
                Language = "en",
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "user1",
                    ArtistName = "Artist",
                    Email = "user1@mail.com",
                    DateOfBirth = new DateOnly(1999, 10, 10)
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                DarkMode = false,
                Language = "de",
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "user2",
                    ArtistName = "Artist2",
                    Email = "user2@mail.com",
                    DateOfBirth = new DateOnly(1999, 10, 10)
                }
            }
        ];

        _userPreferenceDtos = _userPreferences.Select(up => new UserPreferenceDto
        {
            Id = up.Id,
            DarkMode = up.DarkMode,
            Language = up.Language
        }).ToList();

        _unitOfWorkMock.Setup(u => u.UserPreferenceRepository.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_userPreferences);
        _mapperMock.Setup(m => m.Map<IEnumerable<UserPreferenceDto>>(_userPreferences)).Returns(_userPreferenceDtos);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private GetAllUserPreferencesHandler _handler;
    private List<UserPreference> _userPreferences;
    private List<UserPreferenceDto> _userPreferenceDtos;

    [Test]
    public async Task Handle_UserPreferencesExist_ReturnsAllUserPreferenceDtos()
    {
        // Arrange
        var getAllUserPreferencesQuery = new GetAllUserPreferences();

        // Act
        var result = await _handler.Handle(getAllUserPreferencesQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(_userPreferenceDtos.Count));
            Assert.That(result, Is.EquivalentTo(_userPreferenceDtos));
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), LogAction.Get), Times.Once());
    }

    [Test]
    public async Task Handle_NoUserPreferencesExist_ReturnsEmpty()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.UserPreferenceRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        var getAllUserPreferencesQuery = new GetAllUserPreferences();

        // Act
        var result = await _handler.Handle(getAllUserPreferencesQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(UserPreference), LogAction.Get), Times.Once());
    }
}
