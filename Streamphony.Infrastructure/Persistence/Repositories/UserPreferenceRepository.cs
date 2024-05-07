using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class UserPreferenceRepository(ApplicationDbContext context) : Repository<UserPreference>(context), IUserPreferenceRepository
{
}