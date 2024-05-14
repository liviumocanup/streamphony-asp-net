using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Infrastructure.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<string> Register(User user, string password, string firstName, string lastName, string roleEnum)
    {
        await _userManager.CreateAsync(user, password);

        var newClaims = new List<Claim>
        {
            new("FirstName", firstName),
            new("LastName", lastName)
        };

        var role = await _roleManager.FindByNameAsync(roleEnum);
        if (role == null)
        {
            await _roleManager.CreateAsync(new Role(roleEnum));
        }
        await _userManager.AddToRoleAsync(user, roleEnum);
        newClaims.Add(new Claim(ClaimTypes.Role, roleEnum));

        await _userManager.AddClaimsAsync(user, newClaims);

        var claimsIdentity = GetClaimsIdentity(user, newClaims);

        var token = _tokenService.CreateSecurityToken(claimsIdentity);
        return _tokenService.WriteToken(token);
    }

    public async Task<string?> Login(User user, string password)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) return null;

        var claims = _userManager.GetClaimsAsync(user).Result;
        var roles = _userManager.GetRolesAsync(user).Result;

        var claimsIdentity = GetClaimsIdentity(user, (List<Claim>)claims);
        foreach (var role in roles)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        var token = _tokenService.CreateSecurityToken(claimsIdentity);
        return _tokenService.WriteToken(token);
    }

    private static ClaimsIdentity GetClaimsIdentity(User user, List<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new (JwtRegisteredClaimNames.Sub, user.UserName ?? throw new ArgumentNullException(user.UserName)),
            new (JwtRegisteredClaimNames.Email, user.Email ?? throw new ArgumentNullException(user.Email))
        });

        claimsIdentity.AddClaims(claims);

        return claimsIdentity;
    }
}
