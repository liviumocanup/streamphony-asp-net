using System.Linq.Expressions;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class SongRepository(ApplicationDbContext context) : Repository<Song>(context), ISongRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task DeleteWhere(Expression<Func<Song, bool>> predicate, CancellationToken cancellationToken)
    {
        var songs = _context.Songs.Where(predicate);
        _context.Songs.RemoveRange(songs);
        await _context.SaveChangesAsync(cancellationToken);
    }
}