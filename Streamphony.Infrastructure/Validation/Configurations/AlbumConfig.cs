using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Validation.Configurations;

public class AlbumConfig : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.Property(a => a.Title).IsRequired().HasMaxLength(50);
        builder.HasIndex(a => new { a.Title, a.OwnerId }).IsUnique();
        builder.Property(a => a.CoverImageUrl).HasMaxLength(1000);

        builder.HasOne(a => a.Owner)
                .WithMany(u => u.OwnedAlbums)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Songs)
                .WithOne(s => s.Album)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}
