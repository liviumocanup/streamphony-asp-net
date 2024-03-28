namespace Streamphony.Application.DTOs
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid OwnerId { get; set; }
        public HashSet<SongDto> Songs { get; set; }
    }
}