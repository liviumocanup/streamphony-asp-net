using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Streamphony.Infrastructure.Options;

namespace Streamphony.Infrastructure.ServiceProviders.Auth;

public class TokenService(IOptions<AuthSettings> jwtOptions) : ITokenService
{
    private readonly AuthSettings _settings = jwtOptions.Value;

    private static JwtSecurityTokenHandler TokenHandler => new();

    public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
    {
        var tokenDescriptor = GetTokenDescriptor(identity);

        return TokenHandler.CreateToken(tokenDescriptor);
    }

    public string WriteToken(SecurityToken token)
    {
        return TokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
    {
        return new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddDays(7),
            // Audience = _settings.Audiences[0],
            // Issuer = _settings.Issuer,
            // SigningCredentials = new SigningCredentials(_settings.GetSymmetricSecurityKey(),
            //     SecurityAlgorithms.HmacSha256Signature)
        };
    }
}
