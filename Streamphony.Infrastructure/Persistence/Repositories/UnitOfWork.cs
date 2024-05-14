using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IArtistRepository artistRepository,
        ISongRepository songRepository, IAlbumRepository albumRepository,
        IGenreRepository genreRepository, IPreferenceRepository preferenceRepository)
    {
        _context = context;
        ArtistRepository = artistRepository;
        SongRepository = songRepository;
        AlbumRepository = albumRepository;
        GenreRepository = genreRepository;
        PreferenceRepository = preferenceRepository;
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.RollbackTransactionAsync(cancellationToken);
    }

    public IArtistRepository ArtistRepository { get; }
    public ISongRepository SongRepository { get; }
    public IAlbumRepository AlbumRepository { get; }
    public IGenreRepository GenreRepository { get; }
    public IPreferenceRepository PreferenceRepository { get; }
}
