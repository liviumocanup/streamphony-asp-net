namespace Streamphony.Domain.Models;

public class Album : BaseEntity
{
    public required string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan TotalDuration { get; set; }
    
    public Guid CoverBlobId { get; set; }
    public BlobFile CoverBlob { get; set; } = default!;
    
    public ICollection<AlbumArtist> Collaborators { get; set; } = new HashSet<AlbumArtist>();
    public ICollection<Song> Songs { get; set; } = new HashSet<Song>();
    
    public Guid OwnerId { get; set; }
    public Artist Owner { get; set; } = default!;

    public TimeSpan CalculateTotalDuration()
    {
        return new TimeSpan(Songs.Sum(song => song.Duration.Ticks));
    }
}
