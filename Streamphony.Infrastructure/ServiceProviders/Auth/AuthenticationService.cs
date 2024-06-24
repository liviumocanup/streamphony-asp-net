using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Infrastructure.ServiceProviders.Auth;

public class AuthenticationService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    RoleManager<Role> roleManager,
    ITokenService tokenService)
    : IAuthenticationService
{
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<string> Register(Guid userId, string firstName, string lastName, string roleEnum)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) throw new ArgumentNullException(firstName);

        var newClaims = new List<Claim>
        {
            new("FirstName", firstName),
            new("LastName", lastName),
            new("ArtistId", "")
        };

        var role = await _roleManager.FindByNameAsync(roleEnum);
        if (role == null) await _roleManager.CreateAsync(new Role(roleEnum));
        await _userManager.AddToRoleAsync(user, roleEnum);
        newClaims.Add(new Claim(ClaimTypes.Role, roleEnum));

        await _userManager.AddClaimsAsync(user, newClaims);

        var claimsIdentity = GetClaimsIdentity(user, newClaims);

        var token = _tokenService.CreateSecurityToken(claimsIdentity);
        return _tokenService.WriteToken(token);
    }

    public async Task<string?> Login(Guid userId, string password)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) return null;

        var claims = _userManager.GetClaimsAsync(user).Result;
        var roles = _userManager.GetRolesAsync(user).Result;

        var claimsIdentity = GetClaimsIdentity(user, (List<Claim>)claims);
        foreach (var role in roles) claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

        var token = _tokenService.CreateSecurityToken(claimsIdentity);
        return _tokenService.WriteToken(token);
    }

    public async Task<string?> RefreshToken(Guid userId)
    {
        var userUpdated = await _userManager.FindByIdAsync(userId.ToString());
        if (userUpdated == null) return null;

        var claims = await _userManager.GetClaimsAsync(userUpdated);
        var roles = await _userManager.GetRolesAsync(userUpdated);

        var artistIdClaim = claims.FirstOrDefault(c => c.Type == "ArtistId");
        if (artistIdClaim != null)
        {
            claims.Remove(artistIdClaim);
        }

        claims.Add(new Claim("ArtistId", userUpdated.ArtistId?.ToString() ?? ""));

        var newClaims = await UpdateUserRoleBasedOnArtistId(userUpdated, claims);
        await _userManager.AddClaimsAsync(userUpdated, newClaims);

        var claimsIdentity = GetClaimsIdentity(userUpdated, newClaims.ToList());
        foreach (var role in roles) claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

        var token = _tokenService.CreateSecurityToken(claimsIdentity);
        return _tokenService.WriteToken(token);
    }

    private static ClaimsIdentity GetClaimsIdentity(User user, List<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.UserName ?? throw new ArgumentNullException(user.UserName)),
            new(JwtRegisteredClaimNames.Email, user.Email ?? throw new ArgumentNullException(user.Email))
        });

        claimsIdentity.AddClaims(claims);

        return claimsIdentity;
    }

    private async Task<IList<Claim>> UpdateUserRoleBasedOnArtistId(User user, IList<Claim> claims)
    {
        var hasArtistId = user.ArtistId.HasValue;
        var currentRoles = await _userManager.GetRolesAsync(user);
        var artistRole = RoleEnum.Artist.ToString();

        var role = await _roleManager.FindByNameAsync(artistRole);
        if (role == null) await _roleManager.CreateAsync(new Role(artistRole));

        switch (hasArtistId)
        {
            case true when !currentRoles.Contains(artistRole):
                await _userManager.AddToRoleAsync(user, artistRole);
                claims.Add(new Claim(ClaimTypes.Role, artistRole));
                break;
            case false when currentRoles.Contains(artistRole):
                await _userManager.RemoveFromRoleAsync(user, artistRole);
                var artistClaim = claims.FirstOrDefault(c => c.Type == "ArtistId");
                if (artistClaim != null)
                {
                    claims.Remove(artistClaim);
                }
                break;
        }

        return claims;
    }
}
