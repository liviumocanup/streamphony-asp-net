using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.App.Preferences.Responses;
using Streamphony.Application.App.Songs.DTOs;

namespace Streamphony.Application.App.Artists.Responses;

public class ArtistDetailsDto : ArtistDto
{
    public ICollection<SongDto?> UploadedSongs { get; set; } = [];
    public ICollection<AlbumDto?> OwnedAlbums { get; set; } = [];
    public PreferenceDto? Preference { get; set; }
}
