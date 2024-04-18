using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Persistence.Configurations
{
    public class UserPreferencesConfig : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> builder)
        {
            builder.HasKey(up => up.Id);

            builder.HasOne(up => up.User)
                .WithOne(u => u.Preferences)
                .HasForeignKey<UserPreference>(up => up.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
