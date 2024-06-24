namespace Streamphony.Application.App.Artists.DTOs;

public class ArtistDto
{
    public Guid Id { get; set; }
    public required string StageName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
