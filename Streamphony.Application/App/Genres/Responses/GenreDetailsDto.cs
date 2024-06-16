using Streamphony.Application.App.Songs.DTOs;

namespace Streamphony.Application.App.Genres.Responses;

public class GenreDetailsDto : GenreDto
{
    public ICollection<SongDto?> Songs { get; set; } = [];
}
