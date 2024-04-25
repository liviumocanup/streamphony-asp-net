namespace Streamphony.Domain.Models
{
    public class AlbumArtist
    {
        public Guid AlbumId { get; set; }
        public Album Album { get; set; } = default!;

        public Guid ArtistId { get; set; }
        public User Artist { get; set; } = default!;

        public ArtistRole Role { get; set; }
    }
}
