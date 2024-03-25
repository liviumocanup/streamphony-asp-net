public class SongDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public TimeSpan Duration { get; set; }
    public Guid OwnerId { get; set; }
    public Guid GenreId { get; set; }
    public Guid AlbumId { get; set; }
}
