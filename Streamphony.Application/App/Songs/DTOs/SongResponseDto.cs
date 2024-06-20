namespace Streamphony.Application.App.Songs.DTOs;

public class SongResponseDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string CoverUrl { get; set; }
    public required string AudioUrl { get; set; }
    public TimeSpan Duration { get; set; }
    public Guid? GenreId { get; set; }
    public Guid? AlbumId { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
