using Mapster;
using Streamphony.Domain.Models;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.Infrastructure.Mapping.Profiles;

public static class MapsterConfig
{
    public static TypeAdapterConfig GlobalConfig { get; }

    static MapsterConfig()
    {
        GlobalConfig = new TypeAdapterConfig();

        GlobalConfig.NewConfig<User, UserCreationDto>().TwoWays();
        GlobalConfig.NewConfig<User, UserDto>().TwoWays();
        GlobalConfig.NewConfig<User, UserDetailsDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Song, SongCreationDto>().TwoWays();
        GlobalConfig.NewConfig<Song, SongDto>().TwoWays();

        GlobalConfig.NewConfig<Genre, GenreCreationDto>().TwoWays();
        GlobalConfig.NewConfig<Genre, GenreDto>().TwoWays();
        GlobalConfig.NewConfig<Genre, GenreDetailsDto>().PreserveReference(true);

        GlobalConfig.NewConfig<UserPreference, UserPreferenceDto>().TwoWays();

        GlobalConfig.NewConfig<Album, AlbumCreationDto>().TwoWays();
        GlobalConfig.NewConfig<Album, AlbumDto>().TwoWays();
        GlobalConfig.NewConfig<Album, AlbumDetailsDto>().PreserveReference(true);

        GlobalConfig.Compile();
    }
}
