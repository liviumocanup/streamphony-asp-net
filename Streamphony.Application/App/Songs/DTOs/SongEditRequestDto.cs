namespace Streamphony.Application.App.Songs.DTOs;

public class SongEditRequestDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public Guid CoverBlobId { get; set; }
}
