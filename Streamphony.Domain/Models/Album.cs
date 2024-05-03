namespace Streamphony.Domain.Models;

public class Album : BaseEntity
{
    public required string Title { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = default!;
    public ICollection<AlbumArtist> Artists { get; set; } = new HashSet<AlbumArtist>();
    public ICollection<Song> Songs { get; set; } = new HashSet<Song>();

    public TimeSpan CalculateTotalDuration()
    {
        return new TimeSpan(Songs.Sum(song => song.Duration.Ticks));
    }
}
