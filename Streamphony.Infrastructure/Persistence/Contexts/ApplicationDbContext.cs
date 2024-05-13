using Microsoft.EntityFrameworkCore;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Configurations;

namespace Streamphony.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<AlbumArtist> AlbumArtists { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<UserPreference> UserPreferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var assembly = typeof(UserPreferencesConfig).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
