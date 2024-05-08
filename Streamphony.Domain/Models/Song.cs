namespace Streamphony.Domain.Models;

public class Song : BaseEntity
{
    public required string Title { get; set; }
    public TimeSpan Duration { get; set; }
    public required string Url { get; set; }

    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = default!;

    public Guid? GenreId { get; set; }
    public Genre? Genre { get; set; } = default!;

    public Guid? AlbumId { get; set; }
    public Album? Album { get; set; }
}