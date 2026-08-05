using Kalmia.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kalmia.Data;

public static class SeedData
{
    public static async Task SeedAsync(KalmiaDbContext dbContext)
    {
        if (await dbContext.Activities.AnyAsync()) return;

        dbContext.Activities.AddRange(
            new Activity
            {
                Name = "dupont",
                ActivityType = ActivityType.MtnBike,
                ActivityDate = new DateOnly(2026, 7, 28),
                StartTime = new TimeOnly(7, 45),
                DurationSeconds = 5400, // 1h30m
                DistanceMeters = 19312, // 12miles
                ElevationGainMeters = 500, // 1640ft
                Description = "fav 90min dupont route (jim branch / ridgeline area)"
            },
            new Activity
            {
                Name = "whitewater center",
                ActivityType = ActivityType.MtnBike,
                ActivityDate = new DateOnly(2026, 7, 30),
                StartTime = new TimeOnly(9, 30),
                DurationSeconds = 4980, // 1h23m
                DistanceMeters = 16093, // 10miles
                ElevationGainMeters = 375, // 1230ft
                Description = "catawba route (north & south + lake loop)"
            },
            new Activity
            {
                Name = "brevard grvl",
                ActivityType = ActivityType.GrvlRide,
                ActivityDate = new DateOnly(2026, 8, 1),
                StartTime = new TimeOnly(8, 00),
                DurationSeconds = 9360, // 2h36m
                DistanceMeters = 51500, // 32miles
                ElevationGainMeters = 992, // 3254ft
                Description = "avery up, 276 paved down, headwaters + cove creek up, 276 paved back down"
            }
        );

        await dbContext.SaveChangesAsync();
    }
}