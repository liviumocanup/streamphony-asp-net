using AutoMapper;
using Streamphony.Domain.Models;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Song, SongDto>().ReverseMap();
        }
    }
}
