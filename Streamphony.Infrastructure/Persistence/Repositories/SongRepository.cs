using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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
    
    public async Task<IEnumerable<Song>> GetByOwnerId(Guid ownerId, CancellationToken cancellationToken)
    {
        return await _context.Songs.Where(song => song.OwnerId == ownerId).ToListAsync(cancellationToken);
    }

    public async Task<Song?> GetByOwnerIdAndTitle(Guid ownerId, string title, CancellationToken cancellationToken)
    {
        return await _context.Songs.FirstOrDefaultAsync(song => song.OwnerId == ownerId && song.Title == title,
            cancellationToken);
    }

    public async Task<IEnumerable<Song>> GetByOwnerIdAndTitleWhereIdNotEqual(Guid ownerId, string title, Guid songId,
        CancellationToken cancellationToken)
    {
        return await _context.Songs.Where(song => song.OwnerId == ownerId && song.Title == title && song.Id != songId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Song>> GetByIdsWithBlobs(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return await IncludeBlobs()
            .Where(song => ids.Contains(song.Id))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Song>> GetByOwnerIdWithBlobs(Guid ownerId, CancellationToken cancellationToken)
    {
        return await IncludeRelatedEntities()
            .Where(song => song.OwnerId == ownerId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Song?> GetByIdWithBlobs(Guid songId, CancellationToken cancellationToken)
    {
        return await IncludeRelatedEntities()
            .SingleOrDefaultAsync(song => song.Id == songId, cancellationToken);
    }
    
    private IQueryable<Song> IncludeBlobs()
    {
        return _context.Songs
            .Include(song => song.AudioBlob)
            .Include(song => song.CoverBlob);
    }
    
    private IQueryable<Song> IncludeRelatedEntities()
    {
        return IncludeBlobs()
            .Include(song => song.Album)
            .Include(song => song.Genre)
            .Include(song => song.Owner)
            .ThenInclude(owner => owner.ProfilePictureBlob);
    }
}
