using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<User?> GetByUsername(string username, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Username == username, cancellationToken);
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetByUsernameOrEmail(string username, string email, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(user => user.Username == username || user.Email == email).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetByUsernameOrEmailWhereIdNotEqual(string username, string email, Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(user => (user.Username == username || user.Email == email) && user.Id != userId).ToListAsync(cancellationToken);
    }
}