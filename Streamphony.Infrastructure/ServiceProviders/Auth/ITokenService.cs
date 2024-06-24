using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Streamphony.Infrastructure.ServiceProviders.Auth;

public interface ITokenService
{
    public SecurityToken CreateSecurityToken(ClaimsIdentity identity);
    public string WriteToken(SecurityToken token);
}
