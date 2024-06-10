using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.Abstractions.Services;

public interface IUserManagerProvider
{
    public Task<User?> FindByNameAsync(string userName);
    public Task<User?> FindByEmailAsync(string userName);
}
