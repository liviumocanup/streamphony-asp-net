using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class UnitOfWork(
    ApplicationDbContext context,
    IArtistRepository artistRepository,
    ISongRepository songRepository,
    IAlbumRepository albumRepository,
    IGenreRepository genreRepository,
    IPreferenceRepository preferenceRepository,
    IBlobRepository blobRepository,
    IAlbumArtistRepository albumArtistRepository)
    : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

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

    public IArtistRepository ArtistRepository { get; } = artistRepository;
    public ISongRepository SongRepository { get; } = songRepository;
    public IAlbumRepository AlbumRepository { get; } = albumRepository;
    public IGenreRepository GenreRepository { get; } = genreRepository;
    public IPreferenceRepository PreferenceRepository { get; } = preferenceRepository;
    public IBlobRepository BlobRepository { get; } = blobRepository;
    public IAlbumArtistRepository AlbumArtistRepository { get; } = albumArtistRepository;
}
