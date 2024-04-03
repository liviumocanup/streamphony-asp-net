namespace Streamphony.Application.App.Songs.Responses
{
    public class SongDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid OwnerId { get; set; }
        public required string Url { get; set; }

        // public Guid GenreId { get; set; }
        // public Guid AlbumId { get; set; }
    }
}