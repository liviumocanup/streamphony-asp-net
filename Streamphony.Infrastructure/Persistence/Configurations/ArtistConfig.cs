using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations;

public class ArtistConfig : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.Property(artist => artist.StageName).IsRequired().HasMaxLength(100);
        builder.Property(artist => artist.DateOfBirth).IsRequired();
        
        builder.HasOne(artist => artist.ProfilePictureBlob)
            .WithOne()
            .HasForeignKey<Artist>(artist => artist.ProfilePictureBlobId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(artist => artist.User)
            .WithOne(user => user.Artist)
            .HasForeignKey<Artist>(artist => artist.UserId)
            .OnDelete(DeleteBehavior.Cascade);

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
