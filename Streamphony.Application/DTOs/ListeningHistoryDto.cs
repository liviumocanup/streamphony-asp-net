namespace Streamphony.Application.DTOs
{
    public class ListeningHistoryDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SongId { get; set; }
        public DateTime LastPlayedAt { get; set; }
    }
}