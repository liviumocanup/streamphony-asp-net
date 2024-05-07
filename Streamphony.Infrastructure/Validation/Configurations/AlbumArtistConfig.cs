using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Validation.Configurations;

public class AlbumArtistConfig : IEntityTypeConfiguration<AlbumArtist>
{
    public void Configure(EntityTypeBuilder<AlbumArtist> builder)
    {
        builder.Property(aa => aa.Role).HasConversion<string>().HasMaxLength(50);

        builder.HasKey(aa => new { aa.AlbumId, aa.ArtistId, aa.Role });

        builder.HasOne(aa => aa.Album)
                .WithMany(a => a.Artists)
                .HasForeignKey(aa => aa.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(aa => aa.Artist)
                .WithMany(u => u.AlbumContributions)
                .HasForeignKey(aa => aa.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
