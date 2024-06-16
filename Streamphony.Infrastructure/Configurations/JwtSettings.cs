using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Streamphony.Infrastructure.Configurations;

public class JwtSettings
{
    public string SigningKey { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public string[] Audiences { get; init; } = [];

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
    }
}
