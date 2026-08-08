using Kalmia.Core.Common;

namespace Kalmia.Core.Entities;

// ------------------------------------------------------------------------------------------------
// main Activity entity class to map to db table
// ------------------------------------------------------------------------------------------------
public class Activity
{
    public int Id { get; set; }
    public int UserProfileId { get; set; }
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
// manual mappings for entity <--> dto
// ------------------------------------------------------------------------------------------------
public static class ActivityMappingExtensions
{
    public static ActivityDto ToDto(this Activity entity)
    {
        return new ActivityDto(
            Id: entity.Id,
            Name: entity.Name,
            ActivityType: entity.ActivityType,
            ActivityDate: entity.ActivityDate,
            StartTime: entity.StartTime,
            DurationSeconds: entity.DurationSeconds,
            DistanceMeters: entity.DistanceMeters,
            ElevationGainMeters: entity.ElevationGainMeters,
            Description: entity.Description
        );
    }

    public static Activity ToEntity(this ActivityDto dto)
    {
        var entity = new Activity();
        entity.SetFieldsFrom(dto);
        return entity;
    }

    // public static void ApplyUpdate(this Activity activity, ActivityDto dto)
    // {
    //     activity.SetFieldsFrom(dto);
    // }

    // create this method instead to be used from the service directly during update and in ToEntity during create
    public static void SetFieldsFrom(this Activity entity, ActivityDto dto)
    {
        entity.Name = dto.Name;
        entity.ActivityType = dto.ActivityType;
        entity.ActivityDate = dto.ActivityDate;
        entity.StartTime = dto.StartTime;
        entity.DurationSeconds = dto.DurationSeconds;
        entity.DistanceMeters = dto.DistanceMeters;
        entity.ElevationGainMeters = dto.ElevationGainMeters;
        entity.Description = dto.Description;
    }
}