using Microsoft.AspNetCore.Identity;

namespace Streamphony.Domain.Models.Auth;

public class User : IdentityUser<Guid>
{
    public Guid? ArtistId { get; set; }
    public Artist? Artist { get; set; }
}
