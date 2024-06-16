using Streamphony.Application.App.Songs.DTOs;

namespace Streamphony.Application.App.Albums.Responses;

public class AlbumDetailsDto : AlbumDto
{
    public HashSet<SongDto?> Songs { get; set; } = [];
}
