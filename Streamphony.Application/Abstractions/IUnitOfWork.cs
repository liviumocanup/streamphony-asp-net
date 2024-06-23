using Streamphony.Application.Abstractions.Repositories;

namespace Streamphony.Application.Abstractions;

public interface IUnitOfWork
{
    public ISongRepository SongRepository { get; }
    public IArtistRepository ArtistRepository { get; }
    public IAlbumRepository AlbumRepository { get; }
    public IGenreRepository GenreRepository { get; }
    public IPreferenceRepository PreferenceRepository { get; }
    public IBlobRepository BlobRepository { get; }
    public IAlbumArtistRepository AlbumArtistRepository { get; }
    Task SaveAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
