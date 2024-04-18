using Streamphony.Application.Abstractions.Repositories;

namespace Streamphony.Application.Abstractions;

public interface IUnitOfWork
{
    Task SaveAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    public ISongRepository SongRepository { get; }
    public IUserRepository UserRepository { get; }
    public IAlbumRepository AlbumRepository { get; }
    public IGenreRepository GenreRepository { get; }
    public IUserPreferenceRepository UserPreferenceRepository { get; }
}