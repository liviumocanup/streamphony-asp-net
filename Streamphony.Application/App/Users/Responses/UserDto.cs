namespace Streamphony.Application.App.Users.Responses;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public Guid ArtistId { get; set; }
}
