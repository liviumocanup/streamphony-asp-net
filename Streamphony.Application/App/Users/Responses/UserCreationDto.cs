namespace Streamphony.Application.App.Users.Responses;

public class UserCreationDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string ArtistName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
}