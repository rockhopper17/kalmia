using Kalmia.Core.Common;
using Kalmia.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kalmia.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(KalmiaDbContext dbContext)
    {
        UserProfile? user = await dbContext.UserProfiles.FirstOrDefaultAsync();

        if (user is null)
        {
            user = new UserProfile
            {
                // Id = 1,
                Name = "zagier",
                Email = "drew.navratil@gmail.com",
                HeightCm = 173,
                WeightKg = 68,
                Sex = Sex.Male
            };
            dbContext.UserProfiles.Add(user);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Activities.AnyAsync())
        {
            dbContext.Activities.AddRange(
                new Activity
                {
                    Name = "dupont",
                    UserProfileId = user.Id,
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
                    UserProfileId = user.Id,
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
                    UserProfileId = user.Id,
                    ActivityType = ActivityType.GrvlRide,
                    ActivityDate = new DateOnly(2026, 8, 1),
                    StartTime = new TimeOnly(8, 00),
                    DurationSeconds = 9360, // 2h36m
                    DistanceMeters = 51500, // 32miles
                    ElevationGainMeters = 992, // 3254ft
                    Description = "avery up, 276 paved down, headwaters + cove creek up, 276 paved back down"
                }
            );
        }

        await dbContext.SaveChangesAsync();
    }
}