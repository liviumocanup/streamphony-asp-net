using Mapster;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.App.Preferences.Responses;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Infrastructure.ServiceProviders.Mapping.Profiles;

public static class MapsterConfig
{
    static MapsterConfig()
    {
        GlobalConfig = new TypeAdapterConfig();

        GlobalConfig.NewConfig<RegisterUserDto, User>()
            .Map(dest => dest.UserName, src => src.Username)
            .Map(dest => dest.Email, src => src.Email)
            .IgnoreNonMapped(true);

        GlobalConfig.NewConfig<UserDto, User>()
            .Map(dest => dest.UserName, src => src.Username)
            .PreserveReference(true);

        GlobalConfig.NewConfig<Artist, ArtistCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Artist, ArtistDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Artist, ArtistDetailsDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Song, SongCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Song, SongDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Genre, GenreCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Genre, GenreDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Genre, GenreDetailsDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Preference, PreferenceDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Album, AlbumCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Album, AlbumDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Album, AlbumDetailsDto>().PreserveReference(true);

        GlobalConfig.Compile();
    }

    public static TypeAdapterConfig GlobalConfig { get; }
}
