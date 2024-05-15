using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Users.Queries;

[TestFixture]
public class GetAllUsersTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private GetAllUsersHandler _handler;
    private List<User> _users;
    private List<UserDto> _userDtos;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _handler = new GetAllUsersHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);

        _users =
        [
            new() { Id = Guid.NewGuid(), Username = "user1", Email = "user1@example.com", ArtistName = "Artist1", DateOfBirth = new DateOnly(1985, 5, 15) },
            new() { Id = Guid.NewGuid(), Username = "user2", Email = "user2@example.com", ArtistName = "Artist2", DateOfBirth = new DateOnly(1990, 1, 1) }
        ];

        _userDtos = _users.Select(user => new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            ArtistName = user.ArtistName,
            DateOfBirth = user.DateOfBirth
        }).ToList();

        _unitOfWorkMock.Setup(u => u.UserRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(_users);
        _mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(_users)).Returns(_userDtos);
    }

    [Test]
    public async Task Handle_UsersExist_ReturnsAllUserDtos()
    {
        // Arrange
        var getAllUsersQuery = new GetAllUsers();

        // Act
        var result = await _handler.Handle(getAllUsersQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(_userDtos.Count));
            Assert.That(result, Is.EquivalentTo(_userDtos));
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), LogAction.Get), Times.Once());
    }

    [Test]
    public async Task Handle_NoUsersExist_ReturnsEmpty()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.UserRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(new List<User>());
        var getAllUsersQuery = new GetAllUsers();

        // Act
        var result = await _handler.Handle(getAllUsersQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(User), LogAction.Get), Times.Once());
    }
}
