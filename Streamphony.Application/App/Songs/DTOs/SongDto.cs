namespace Streamphony.Application.App.Songs.DTOs;

public class SongDto : SongRequestDto
{
    public TimeSpan Duration { get; set; }
    public required string Url { get; set; }
    public Guid OwnerId { get; set; }
}
