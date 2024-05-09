using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.App.Albums.Responses;

public class AlbumDetailsDto : AlbumDto
{
    public HashSet<SongDto?> Songs { get; set; } = new HashSet<SongDto?>();
}
