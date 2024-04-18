using AutoMapper;
using Streamphony.Domain.Models;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserCreationDto>().ReverseMap();
            CreateMap<User, UserDetailsDto>().ReverseMap();

            CreateMap<Song, SongDto>().ReverseMap();
            CreateMap<Song, SongCreationDto>().ReverseMap();

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Genre, GenreCreationDto>().ReverseMap();
            CreateMap<Genre, GenreDetailsDto>().ReverseMap();

            CreateMap<UserPreference, UserPreferenceDto>().ReverseMap();

            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<Album, AlbumCreationDto>().ReverseMap();
            CreateMap<Album, AlbumDetailsDto>().ReverseMap();
        }
    }
}
