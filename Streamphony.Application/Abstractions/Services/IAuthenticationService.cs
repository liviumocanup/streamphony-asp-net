using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.Abstractions.Services;

public interface IAuthenticationService
{
    public Task<string> Register(Guid userId, string firstName, string lastName, string roleEnum);
    public Task<string?> Login(Guid userId, string password);
    public Task<string?> RefreshToken(Guid userId);
}
