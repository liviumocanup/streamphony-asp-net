namespace Streamphony.Application.App.Artists.DTOs;

public class ArtistCreationDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Guid ProfilePictureId { get; set; }
}
