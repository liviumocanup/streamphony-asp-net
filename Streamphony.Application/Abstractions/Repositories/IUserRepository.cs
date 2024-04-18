using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsername(string username);
}
