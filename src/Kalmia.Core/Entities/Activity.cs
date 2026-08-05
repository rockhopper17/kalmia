namespace Kalmia.Core.Entities;

// ------------------------------------------------------------------------------------------------
// activity type enum, ie mtb, grvl, etc
// ------------------------------------------------------------------------------------------------
public enum ActivityType
{
    MtnBike = 1,
    GrvlRide = 2,
    RoadRide = 3,
    MixedRide = 4
}

// ------------------------------------------------------------------------------------------------
// main Activity entity class to map to db table
// ------------------------------------------------------------------------------------------------
public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ActivityType ActivityType { get; set; }
    public DateOnly ActivityDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public int DurationSeconds { get; set; }
    public double DistanceMeters { get; set; }
    public double ElevationGainMeters { get; set; }
    public string? Description { get; set; }

    // computed properties
    public TimeSpan Duration => TimeSpan.FromSeconds(DurationSeconds);
    public double DistanceMiles => DistanceMeters / 1609.344;
    public double ElevationGainFeet => ElevationGainMeters * 3.28084;
}

// ------------------------------------------------------------------------------------------------
// Activity DTO (data transfer object)
// - for specifying contract use in the api or other calling layers
// - additional dto forms, such as for UI views, can be created in the calling layer
// - logic for what is allowed to be exposed stays here in the core business logic
// ------------------------------------------------------------------------------------------------
public record ActivityDto(
    int? Id,  // making Id nullable for dto use in create/read/update
    string Name,
    ActivityType ActivityType,
    DateOnly ActivityDate,
    TimeOnly StartTime,
    int DurationSeconds,
    double DistanceMeters,
    double ElevationGainMeters,
    string? Description
);

// ------------------------------------------------------------------------------------------------
// manually mappings for entity <--> dto
// ------------------------------------------------------------------------------------------------
public static class ActivityMappingExtensions
{
    public static ActivityDto ToDto(this Activity activity)
    {
        return new ActivityDto(
            Id: activity.Id,
            Name: activity.Name,
            ActivityType: activity.ActivityType,
            ActivityDate: activity.ActivityDate,
            StartTime: activity.StartTime,
            DurationSeconds: activity.DurationSeconds,
            DistanceMeters: activity.DistanceMeters,
            ElevationGainMeters: activity.ElevationGainMeters,
            Description: activity.Description
        );
    }

    public static Activity ToEntity(this ActivityDto dto)
    {
        var activity = new Activity();
        activity.SetFieldsFrom(dto);
        return activity;
    }

    // public static void ApplyUpdate(this Activity activity, ActivityDto dto)
    // {
    //     activity.SetFieldsFrom(dto);
    // }

    // create this method instead to be used from the service directly during update and in ToEntity during create
    public static void SetFieldsFrom(this Activity activity, ActivityDto dto)
    {
        activity.Name = dto.Name;
        activity.ActivityType = dto.ActivityType;
        activity.ActivityDate = dto.ActivityDate;
        activity.StartTime = dto.StartTime;
        activity.DurationSeconds = dto.DurationSeconds;
        activity.DistanceMeters = dto.DistanceMeters;
        activity.ElevationGainMeters = dto.ElevationGainMeters;
        activity.Description = dto.Description;
    }
}