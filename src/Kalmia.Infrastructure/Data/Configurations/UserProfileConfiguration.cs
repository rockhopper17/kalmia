using Kalmia.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalmia.Infrastructure.Data.Configurations;

public class UserProfileConnfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(256);

        builder.Property(u => u.Sex)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_UserProfiles_HeightCm_NonNegative", "[HeightCm] >= 0");
            t.HasCheckConstraint("CK_UserProfiles_WeightKg_NonNegative", "[WeightKg] >= 0");
        });
    }
}