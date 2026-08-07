using Kalmia.Core.Entities;
using Xunit;

namespace Kalmia.Core.Tests.Entities;

public class ActivityMappingTests
{
    private static ActivityDto ValidDto() => new(
        Id: 7,
        Name: "dupont",
        ActivityType: ActivityType.MtnBike,
        ActivityDate: new DateOnly(2026, 7, 28),
        StartTime: new TimeOnly(7, 45),
        DurationSeconds: 5400, // 1h30m
        DistanceMeters: 19312, // 12miles
        ElevationGainMeters: 500, // 1640ft
        Description: "fav 90min dupont route (jim branch / ridgeline area)"
    );

    private static Activity ValidActivity() => new Activity
    {
        Id = 3,
        Name = "dupont",
        ActivityType = ActivityType.MtnBike,
        ActivityDate = new DateOnly(2026, 7, 28),
        StartTime = new TimeOnly(7, 45),
        DurationSeconds = 5400, // 1h30m
        DistanceMeters = 19312, // 12miles
        ElevationGainMeters = 500, // 1640ft
        Description = "fav 90min dupont route (jim branch / ridgeline area)"
    };

    [Fact]
    public void ToEntity_MapsAllFieldsFromDto()
    {
        var dto = ValidDto();

        var activity = dto.ToEntity();

        Assert.Equal(dto.Name, activity.Name);
        Assert.Equal(dto.ActivityType, activity.ActivityType);
        Assert.Equal(dto.ActivityDate, activity.ActivityDate);
        Assert.Equal(dto.StartTime, activity.StartTime);
        Assert.Equal(dto.DurationSeconds, activity.DurationSeconds);
        Assert.Equal(dto.DistanceMeters, activity.DistanceMeters);
        Assert.Equal(dto.ElevationGainMeters, activity.ElevationGainMeters);
        Assert.Equal(dto.Description, activity.Description);
    }

    [Fact]
    public void ToEntity_DoesNotSetId()
    {
        var activity = ValidDto().ToEntity();

        Assert.Equal(0, activity.Id);
    }

    [Fact]
    public void ToDto_MapsAllFieldsFromEntity()
    {
        var activity = ValidActivity();
        
        var dto = activity.ToDto();

        Assert.Equal(dto.Id, activity.Id);
        Assert.Equal(dto.Name, activity.Name);
        Assert.Equal(dto.ActivityType, activity.ActivityType);
        Assert.Equal(dto.ActivityDate, activity.ActivityDate);
        Assert.Equal(dto.StartTime, activity.StartTime);
        Assert.Equal(dto.DurationSeconds, activity.DurationSeconds);
        Assert.Equal(dto.DistanceMeters, activity.DistanceMeters);
        Assert.Equal(dto.ElevationGainMeters, activity.ElevationGainMeters);
        Assert.Equal(dto.Description, activity.Description);
    }

    [Fact]
    public void SetFieldsFrom_UpdatesExistingActivityWithoutChangingId()
    {
        var activity = new Activity { Id = 7, Name = "Old Name" };
        var dto = ValidDto() with { Name = "New Name" };

        activity.SetFieldsFrom(dto);

        Assert.Equal(7, activity.Id);
        Assert.Equal("New Name", activity.Name);
    }
}