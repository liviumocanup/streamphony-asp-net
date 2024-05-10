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

        GlobalConfig.NewConfig<User, UserCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<User, UserDto>().PreserveReference(true);
        GlobalConfig.NewConfig<User, UserDetailsDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Song, SongCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Song, SongDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Genre, GenreCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Genre, GenreDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Genre, GenreDetailsDto>().PreserveReference(true);

        GlobalConfig.NewConfig<UserPreference, UserPreferenceDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Album, AlbumCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Album, AlbumDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Album, AlbumDetailsDto>().PreserveReference(true);

        GlobalConfig.Compile();
    }
}
