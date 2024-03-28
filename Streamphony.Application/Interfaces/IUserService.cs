using Streamphony.Domain.Models;
using Streamphony.Application.DTOs;

namespace Streamphony.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(Guid id);
    }
}
