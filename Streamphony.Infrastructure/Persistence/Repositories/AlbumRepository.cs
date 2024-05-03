using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class AlbumRepository(ApplicationDbContext context) : Repository<Album>(context), IAlbumRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Album?> GetByTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Albums.FirstOrDefaultAsync(album => album.Title == title, cancellationToken);
    }
}