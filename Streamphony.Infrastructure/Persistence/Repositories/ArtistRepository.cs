using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class ArtistRepository(ApplicationDbContext context) : Repository<Artist>(context), IArtistRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Artist?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Artists.FirstOrDefaultAsync(artist => artist.UserId == userId, cancellationToken);
    }
    
    public async Task<Artist?> GetByOwnerIdWithBlobs(Guid ownerId, CancellationToken cancellationToken)
    {
        return await _context.Artists
            .Include(artist => artist.ProfilePictureBlob)
            .FirstOrDefaultAsync(artist => artist.UserId == ownerId, cancellationToken);
    }
}
