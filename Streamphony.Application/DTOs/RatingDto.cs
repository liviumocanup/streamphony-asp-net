namespace Streamphony.Application.DTOs
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public Guid UserId { get; set; }
        public Guid SongId { get; set; }
    }
}