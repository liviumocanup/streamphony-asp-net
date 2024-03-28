using AutoMapper;
using Streamphony.Application.DTOs;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Song, SongDto>().ReverseMap();
        }
    }
}
