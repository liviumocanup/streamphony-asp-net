using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;
using Streamphony.Infrastructure.Persistence.Configurations;
using Streamphony.Infrastructure.Persistence.Constants;

namespace Streamphony.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<AlbumArtist> AlbumArtists { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Preference> Preferences { get; set; }
    public DbSet<BlobFile> BlobFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var assembly = typeof(PreferencesConfig).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        ApplyIdentityMapConfiguration(modelBuilder);
    }

    private static void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users", SchemaConstants.Auth);
        modelBuilder.Entity<UserClaim>().ToTable("UserClaims", SchemaConstants.Auth);
        modelBuilder.Entity<UserLogin>().ToTable("UserLogins", SchemaConstants.Auth);
        modelBuilder.Entity<UserToken>().ToTable("UserRoles", SchemaConstants.Auth);
        modelBuilder.Entity<Role>().ToTable("Roles", SchemaConstants.Auth);
        modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", SchemaConstants.Auth);
        modelBuilder.Entity<UserRole>().ToTable("UserRole", SchemaConstants.Auth);
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        var currentTime = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = currentTime;
            }

            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.UpdatedAt = currentTime;
            }
        }
    }
}
