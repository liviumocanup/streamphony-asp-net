using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository,
            ISongRepository songRepository, IAlbumRepository albumRepository,
            IGenreRepository genreRepository, IUserPreferenceRepository userPreferenceRepository)
        {
            _context = context;
            UserRepository = userRepository;
            SongRepository = songRepository;
            AlbumRepository = albumRepository;
            GenreRepository = genreRepository;
            UserPreferenceRepository = userPreferenceRepository;
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

        public IUserRepository UserRepository { get; }
        public ISongRepository SongRepository { get; }
        public IAlbumRepository AlbumRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IUserPreferenceRepository UserPreferenceRepository { get; }
    }
}