using Kalmia.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalmia.Infrastructure.Data.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.ActivityType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.ActivityDate)
            .IsRequired()
            .HasDefaultValueSql("CAST(GETUTCDATE() AS DATE)");

        builder.Property(a => a.StartTime)
            .IsRequired()
            .HasDefaultValue(new TimeOnly(0, 0));

        builder.Property(a => a.DurationSeconds)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(a => a.DistanceMeters)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(a => a.ElevationGainMeters)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(a => a.Description)
            .HasMaxLength(2000);
    }
}