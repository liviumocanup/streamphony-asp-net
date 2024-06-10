using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations;

public class GenreConfig : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(g => g.Name).IsRequired().HasMaxLength(50);
        builder.HasIndex(g => g.Name).IsUnique();
        builder.Property(g => g.Description).IsRequired().HasMaxLength(1000);

        builder.HasMany(g => g.Songs)
            .WithOne(s => s.Genre)
            .HasForeignKey(s => s.GenreId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
