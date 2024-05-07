using Streamphony.Domain.Models;

namespace Streamphony.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsername(string username, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<User?> GetByUsernameOrEmail(string username, string email, CancellationToken cancellationToken);
    Task<User?> GetByUsernameOrEmailWhereIdNotEqual(string username, string email, Guid userId, CancellationToken cancellationToken);
}
