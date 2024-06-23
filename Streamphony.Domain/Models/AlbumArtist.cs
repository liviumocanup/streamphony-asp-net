namespace Streamphony.Domain.Models;

public class AlbumArtist : BaseEntity
{
    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = default!;

    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = default!;

    public ArtistRole Role { get; set; }
}
