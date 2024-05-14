using Streamphony.Application.App.Auth.Responses;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.Abstractions.Services;

public interface IAuthenticationService
{
    public Task<string> Register(User user, string password, string firstName, string lastName, string roleEnum);
    public Task<string?> Login(User user, string password);
}
