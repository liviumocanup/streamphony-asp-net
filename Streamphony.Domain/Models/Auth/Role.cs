using Microsoft.AspNetCore.Identity;

namespace Streamphony.Domain.Models.Auth;

public class Role(string name) : IdentityRole<Guid>(name)
{
}
