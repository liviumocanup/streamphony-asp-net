using AutoMapper;
using Moq;
using NuGet.Frameworks;
using Streamphony.Application.DTOs;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository> _mockRepository = new Mock<IRepository>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ILoggingService> _mockLoggingService = new Mock<ILoggingService>();
        private readonly UserService _userService;
        private readonly UserDto _userDto;
        private readonly User _user;

        public UserServiceTests()
        {
            _userService = new UserService(_mockRepository.Object, _mockMapper.Object, _mockLoggingService.Object);
            _userDto = new UserDto { Id = Guid.NewGuid(), ArtistName = "Test Artist" };
            _user = new User { Id = _userDto.Id, ArtistName = _userDto.ArtistName };
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnCreatedUser_WhenSuccess()
        {
            var user = _user;
            var userDto = _userDto;
            _mockMapper.Setup(m => m.Map<User>(It.IsAny<UserDto>())).Returns(user);
            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(userDto);
            _mockRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _userService.CreateUserAsync(userDto);

            Assert.Multiple(() =>
            {
                Assert.Equal(userDto.Id, result.Id);
                Assert.Equal(userDto.ArtistName, result.ArtistName);
                _mockMapper.Verify(m => m.Map<User>(It.IsAny<UserDto>()), Times.Once);
                _mockMapper.Verify(m => m.Map<UserDto>(It.IsAny<User>()), Times.Once);
                _mockRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
                _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
                _mockLoggingService.Verify(l => l.LogAsync(It.Is<string>(s => s.Contains("success")), It.IsAny<string>()), Times.Once);
            });
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowException_WhenUserAlreadyExists()
        {
            var userDto = _userDto;
            var user = _user;
            _mockMapper.Setup(m => m.Map<User>(It.IsAny<UserDto>())).Returns(user);
            _mockRepository.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new Exception("PK violation", new InvalidOperationException()));

            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateUserAsync(userDto));

            Assert.Multiple(() =>
            {
                Assert.Contains("PK violation", exception.Message);
                _mockLoggingService.Verify(l => l.LogAsync(It.Is<string>(s => s.Contains("failure")), It.IsAny<string>()), Times.Once);
                _mockRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
            });
        }


        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers_WhenSuccess()
        {
            var users = new List<User> { _user };
            var userDtos = new List<UserDto> { _userDto };
            _mockRepository.Setup(r => r.GetAll<User>()).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>())).Returns(userDtos);

            var result = await _userService.GetAllUsersAsync();

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.Equal(users.Count, result.Count());
                _mockLoggingService.Verify(l => l.LogAsync(It.Is<string>(s => s.Contains("success")), It.IsAny<string>()), Times.Once);
            });
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var user = _user;
            var userId = user.Id;
            var userDto = _userDto;
            _mockRepository.Setup(r => r.GetById<User>(userId)).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(userDto);

            var result = await _userService.GetUserByIdAsync(userId);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.Equal(userId, result.Id);
                _mockLoggingService.Verify(l => l.LogAsync(It.Is<string>(s => s.Contains("success")), It.IsAny<string>()), Times.Once);
            });
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var userId = new Guid();
            _mockRepository.Setup(r => r.GetById<User>(userId)).ReturnsAsync(default(User?)!);

            var result = await _userService.GetUserByIdAsync(userId);

            Assert.Multiple(() =>
            {
                Assert.Null(result);
                _mockLoggingService.Verify(l => l.LogAsync(It.Is<string>(s => s.Contains("failure")), It.IsAny<string>()), Times.Once);
            });
        }
    }
}