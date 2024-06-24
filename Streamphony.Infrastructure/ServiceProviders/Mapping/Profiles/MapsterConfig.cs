using Mapster;
using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.Application.App.BlobStorage.DTOs;
using Streamphony.Application.App.Genres.Responses;
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

        GlobalConfig.NewConfig<User, UserDto>()
            .Map(src => src.Username, dest => dest.UserName)
            .PreserveReference(true);

        GlobalConfig.NewConfig<BlobDto, BlobFile>().PreserveReference(true);

        GlobalConfig.NewConfig<Artist, ArtistCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Artist, ArtistDetailsDto>()
            .Map(src => src.ProfilePictureUrl, dest => dest.ProfilePictureBlob.Url)
            .PreserveReference(true);
        GlobalConfig.NewConfig<Artist, ArtistDto>()
            .Map(src => src.ProfilePictureUrl, dest => dest.ProfilePictureBlob.Url)
            .PreserveReference(true);

        GlobalConfig.NewConfig<Song, SongCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Song, SongEditRequestDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Song, SongDto>()
            .Map(src => src.CoverUrl, dest => dest.CoverBlob.Url)
            .Map(src => src.AudioUrl, dest => dest.AudioBlob.Url)
            .PreserveReference(true);
        GlobalConfig.NewConfig<Song, SongDetailsDto>()
            .Map(src => src.CoverUrl, dest => dest.CoverBlob.Url)
            .Map(src => src.AudioUrl, dest => dest.AudioBlob.Url)
            .Map(src => src.Artist, dest => dest.Owner)
            .PreserveReference(true);

        GlobalConfig.NewConfig<Genre, GenreCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Genre, GenreDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Genre, GenreDetailsDto>().PreserveReference(true);

        GlobalConfig.NewConfig<AlbumArtist, ArtistCollaboratorsDto>().PreserveReference(true);

        GlobalConfig.NewConfig<Album, AlbumCreationDto>().PreserveReference(true);
        GlobalConfig.NewConfig<Album, AlbumDto>()
            .Map(src => src.CoverUrl, dest => dest.CoverBlob.Url)
            .PreserveReference(true);
        GlobalConfig.NewConfig<Album, AlbumDetailsDto>()
            .Map(src => src.CoverUrl, dest => dest.CoverBlob.Url)
            .PreserveReference(true);

        GlobalConfig.Compile();
    }

    public static TypeAdapterConfig GlobalConfig { get; }
}
