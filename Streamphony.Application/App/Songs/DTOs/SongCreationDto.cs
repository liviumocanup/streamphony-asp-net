namespace Streamphony.Application.App.Songs.DTOs;

public class SongCreationDto
{
    public required string Title { get; set; }
    public required Guid CoverFileId { get; set; }
    public required Guid AudioFileId { get; set; }
    public Guid? GenreId { get; set; }
    public Guid? AlbumId { get; set; }
}
