namespace Streamphony.Application.App.Artists.Responses;

public class ArtistCreationDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
}