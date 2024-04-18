using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations
{
    public class SongConfig : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.Property(s => s.Title).IsRequired().HasMaxLength(50);
            builder.HasIndex(s => new { s.Title, s.OwnerId }).IsUnique();
            builder.Property(s => s.Duration).IsRequired();
            builder.Property(a => a.Url).IsRequired().HasMaxLength(1000);

            builder.HasOne(s => s.Owner)
                    .WithMany(u => u.UploadedSongs)
                    .HasForeignKey(s => s.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Genre)
                    .WithMany(g => g.Songs)
                    .HasForeignKey(s => s.GenreId)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.Album)
                    .WithMany(a => a.Songs)
                    .HasForeignKey(s => s.AlbumId)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
