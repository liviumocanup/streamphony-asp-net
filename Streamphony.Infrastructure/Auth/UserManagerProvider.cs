using Microsoft.AspNetCore.Identity;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Infrastructure.Auth;

public class UserManagerProvider(UserManager<User> userManager) : IUserManagerProvider
{
    private readonly UserManager<User> _userManager = userManager;

    public Task<User?> FindByNameAsync(string userName)
    {
        return _userManager.FindByNameAsync(userName);
    }

    public Task<User?> FindByEmailAsync(string userName)
    {
        return _userManager.FindByEmailAsync(userName);
    }
}
