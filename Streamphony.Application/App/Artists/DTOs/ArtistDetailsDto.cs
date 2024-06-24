using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Application.App.Songs.DTOs;

namespace Streamphony.Application.App.Artists.DTOs;

public class ArtistDetailsDto : ArtistDto
{
    public ICollection<SongDto?> UploadedSongs { get; set; } = [];
    ICollection<AlbumDto?> OwnedAlbums { get; set; } = [];
}
