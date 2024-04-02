using AutoMapper;
using Streamphony.Application.DTOs;
using Streamphony.Domain.Models;

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
