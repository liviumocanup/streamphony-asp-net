using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class BlobRepository(ApplicationDbContext context) : Repository<BlobFile>(context), IBlobRepository
{
}
