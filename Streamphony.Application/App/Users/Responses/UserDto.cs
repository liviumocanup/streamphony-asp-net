namespace Streamphony.Application.App.Users.Responses;

public class UserDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public Guid? ArtistId { get; set; }
}
