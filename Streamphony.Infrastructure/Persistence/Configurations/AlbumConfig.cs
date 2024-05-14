using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations;

public class AlbumConfig : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.Property(album => album.Title).IsRequired().HasMaxLength(50);
        builder.HasIndex(album => new { album.Title, album.OwnerId }).IsUnique();
        builder.Property(album => album.CoverImageUrl).HasMaxLength(1000);

        builder.HasOne(album => album.Owner)
                .WithMany(artist => artist.OwnedAlbums)
                .HasForeignKey(album => album.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(album => album.Songs)
                .WithOne(s => s.Album)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}
