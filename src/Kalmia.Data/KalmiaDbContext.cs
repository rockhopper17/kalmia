using Kalmia.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kalmia.Data;

public class KalmiaDbContext : DbContext
{
    public KalmiaDbContext(DbContextOptions<KalmiaDbContext> opt) : base(opt) { }

    public DbSet<Activity> Activities => Set<Activity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KalmiaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}