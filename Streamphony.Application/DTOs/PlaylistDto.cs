using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.DTOs
{
    public class PlaylistDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public bool IsPublic { get; set; }
        public Guid OwnerId { get; set; }
        public HashSet<SongDto>? Songs { get; set; }
    }
}