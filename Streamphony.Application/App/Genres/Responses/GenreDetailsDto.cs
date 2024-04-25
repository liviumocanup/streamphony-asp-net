using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.App.Genres.Responses;

public class GenreDetailsDto : GenreDto
{
    public ICollection<SongDto?> Songs { get; set; } = new HashSet<SongDto?>();
}
