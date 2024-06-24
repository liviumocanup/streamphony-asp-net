using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.Abstractions.Services;

public interface IUserManagerProvider
{
    public Task<User?> FindByNameAsync(string userName);
    public Task<User?> FindByEmailAsync(string userName);
    public Task<User?> FindByIdAsync(string userId);
    public Task CreateAsync(User user, string password);
}
