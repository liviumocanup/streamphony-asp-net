namespace Streamphony.Application.App.Albums.Responses;

public class AlbumCreationDto
{
    public required string Title { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public Guid OwnerId { get; set; }
}
