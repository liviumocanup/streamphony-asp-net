using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.DTOs;

public class AlbumDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public required string CoverUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
