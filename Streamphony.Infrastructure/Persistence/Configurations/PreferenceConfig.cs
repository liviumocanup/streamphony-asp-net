using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations;

public class PreferencesConfig : IEntityTypeConfiguration<Preference>
{
    public void Configure(EntityTypeBuilder<Preference> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Artist)
            .WithOne(artist => artist.Preference)
            .HasForeignKey<Preference>(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
