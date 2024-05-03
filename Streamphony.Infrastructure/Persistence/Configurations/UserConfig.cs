using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username).IsRequired().HasMaxLength(30);
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.ArtistName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.ProfilePictureUrl).HasMaxLength(1000);

        builder.HasMany(u => u.UploadedSongs)
                .WithOne(s => s.Owner)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.OwnedAlbums)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}
