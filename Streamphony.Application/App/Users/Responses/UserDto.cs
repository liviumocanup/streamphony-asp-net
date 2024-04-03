using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.App.Users.Responses
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string ArtistName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public ICollection<SongDto>? UploadedSongs { get; set; }
    }
}