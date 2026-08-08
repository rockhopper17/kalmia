using Kalmia.Core.Entities;
using Kalmia.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kalmia.Infrastructure.Data;

public class KalmiaDbContext : IdentityDbContext<KalmiaUser, IdentityRole<int>, int>
{
    public KalmiaDbContext(DbContextOptions<KalmiaDbContext> opt) : base(opt) { }

    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KalmiaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}