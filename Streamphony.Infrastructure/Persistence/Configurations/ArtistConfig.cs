using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations;

public class ArtistConfig : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.Property(artist => artist.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(artist => artist.LastName).IsRequired().HasMaxLength(50);
        builder.Property(artist => artist.ProfilePictureUrl).HasMaxLength(1000);

        builder.HasMany(artist => artist.UploadedSongs)
            .WithOne(s => s.Owner)
            .HasForeignKey(s => s.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(artist => artist.OwnedAlbums)
            .WithOne(album => album.Owner)
            .HasForeignKey(album => album.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
