using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.DTOs;

public class ArtistCollaboratorsDto
{
    public required ArtistDto Artist { get; set; }
    public required string Role { get; set; }
}
