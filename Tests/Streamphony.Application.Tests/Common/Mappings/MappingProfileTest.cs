using AutoMapper;
using Streamphony.Application.Common.Mappings;
using Streamphony.Application.DTOs;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.Common.Mappings
{
    public class MappingProfileTest
    {
        private readonly MapperConfiguration _configuration;
        private readonly IMapper _mapper;

        public MappingProfileTest()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_UserToUserDto_MapsCorrectly()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                ArtistName = "Artist",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                ProfilePictureUrl = "http://example.com/picture.jpg"
            };

            var userDto = _mapper.Map<UserDto>(user);

            Assert.Multiple(() =>
            {
                Assert.Equal(user.Id, userDto.Id);
                Assert.Equal(user.ArtistName, userDto.ArtistName);
                Assert.Equal(user.DateOfBirth, userDto.DateOfBirth);
                Assert.Equal(user.ProfilePictureUrl, userDto.ProfilePictureUrl);
            });
        }

        [Fact]
        public void AutoMapper_UserDtoToUser_MapsCorrectly()
        {
            var userDto = new UserDto
            {
                Id = Guid.NewGuid(),
                ArtistName = "DTO Artist",
                DateOfBirth = DateTime.UtcNow.AddYears(-25),
                ProfilePictureUrl = "http://example.com/dto_picture.jpg"
            };

            var user = _mapper.Map<User>(userDto);

            Assert.Multiple(() =>
            {
                Assert.Equal(userDto.Id, user.Id);
                Assert.Equal(userDto.ArtistName, user.ArtistName);
                Assert.Equal(userDto.DateOfBirth, user.DateOfBirth);
                Assert.Equal(userDto.ProfilePictureUrl, user.ProfilePictureUrl);
            });
        }

        [Fact]
        public void AutoMapper_SongToSongDto_MapsCorrectly()
        {
            var song = new Song
            {
                Id = Guid.NewGuid(),
                Title = "Song Title",
                Duration = new TimeSpan(0, 3, 45),
                OwnerId = Guid.NewGuid()
            };

            var songDto = _mapper.Map<SongDto>(song);

            Assert.Multiple(() =>
            {
                Assert.Equal(song.Id, songDto.Id);
                Assert.Equal(song.Title, songDto.Title);
                Assert.Equal(song.Duration, songDto.Duration);
                Assert.Equal(song.OwnerId, songDto.OwnerId);
            });
        }

        [Fact]
        public void AutoMapper_SongDtoToSong_MapsCorrectly()
        {
            var songDto = new SongDto
            {
                Id = Guid.NewGuid(),
                Title = "DTO Song Title",
                Duration = new TimeSpan(0, 4, 30),
                OwnerId = Guid.NewGuid()
            };

            var song = _mapper.Map<Song>(songDto);

            Assert.Multiple(() =>
            {
                Assert.Equal(songDto.Id, song.Id);
                Assert.Equal(songDto.Title, song.Title);
                Assert.Equal(songDto.Duration, song.Duration);
                Assert.Equal(songDto.OwnerId, song.OwnerId);
            });
        }
    }
}