namespace Kalmia.Core;

public class Activity
{
    public int Id { get; set; }
    public required string Name { get; set; }
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

public enum ActivityType
{
    MtnBike = 1,
    GrvlRide = 2,
    RoadRide = 3,
    MixedRide = 4
}

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
