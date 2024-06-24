using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations;

public class AlbumArtistConfig : IEntityTypeConfiguration<AlbumArtist>
{
    public void Configure(EntityTypeBuilder<AlbumArtist> builder)
    {
        builder.Property(aa => aa.Role).HasConversion<string>().HasMaxLength(50);
        builder.HasIndex(aa => new { aa.AlbumId, aa.ArtistId, aa.Role }).IsUnique();

        builder.HasOne(aa => aa.Album)
            .WithMany(album => album.Collaborators)
            .HasForeignKey(aa => aa.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(aa => aa.Artist)
            .WithMany(artist => artist.AlbumContributions)
            .HasForeignKey(aa => aa.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
