namespace Streamphony.Application.App.Artists.DTOs;

public class ArtistCreationDto
{
    public required string StageName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Guid ProfilePictureId { get; set; }
}
