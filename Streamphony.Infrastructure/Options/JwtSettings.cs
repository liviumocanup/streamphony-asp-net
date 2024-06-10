using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Streamphony.Infrastructure.Options;

public class JwtSettings
{
    public string SigningKey { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string[] Audiences { get; set; } = default!;

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
    }
}
