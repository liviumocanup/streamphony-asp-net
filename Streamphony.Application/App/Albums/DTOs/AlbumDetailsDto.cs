using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Application.App.Songs.DTOs;

namespace Streamphony.Application.App.Albums.DTOs;

public class AlbumDetailsDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public required string CoverUrl { get; set; }
    public ArtistDto Owner { get; set; } = default!;
    public HashSet<ArtistCollaboratorsDto>? Collaborators { get; set; }
    public HashSet<SongDto>? Songs { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
