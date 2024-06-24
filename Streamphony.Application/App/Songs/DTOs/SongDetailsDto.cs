using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Application.App.BlobStorage.DTOs;
using Streamphony.Application.App.Genres.Responses;

namespace Streamphony.Application.App.Songs.DTOs;

public class SongDetailsDto : SongDto
{
    public ArtistDto Artist { get; set; } = default!;
    public AlbumDto? Album { get; set; }
    public GenreDto? Genre { get; set; }
    public BlobDto CoverBlob { get; set; } = default!;
}
