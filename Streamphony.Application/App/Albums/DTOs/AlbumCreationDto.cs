using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.DTOs;

public class AlbumCreationDto
{
    public required string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public required Guid CoverFileId { get; set; }
    public HashSet<Guid> SongIds { get; set; } = default!;
    public Dictionary<Guid, ArtistRole[]>? Collaborators { get; set; }
}
