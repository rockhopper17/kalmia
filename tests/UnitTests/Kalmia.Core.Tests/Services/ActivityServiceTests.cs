using Kalmia.Core.Common;
using Kalmia.Core.Entities;
using Kalmia.Core.Services;
using Kalmia.Core.Tests.Fakes;
using Xunit;

namespace Kalmia.Core.Tests.Services;

public class ActivityServiceTests
{
    private readonly FakeActivityRepository _repo;
    private readonly ActivityService _srvc;

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

    public ActivityServiceTests()
    {
        _repo = new FakeActivityRepository();
        _srvc = new ActivityService(_repo);
    }

    [Fact]
    public async Task AddAsync_WithValidDto_Succeeds()
    {
        var result = await _srvc.AddAsync(ValidDto());

        Assert.True(result.IsSuccess);
        Assert.Equal("dupont", result.Value!.Name);

        var all = await _repo.GetAllAsync();
        Assert.Single(all);
    }

    [Fact]
    public async Task AddAsync_WithEmptyName_FailsValidationAndDoesNotPersist()
    {
        var dto = ValidDto() with { Name = "" };

        var result = await _srvc.AddAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "NAME_REQUIRED");

        var all = await _repo.GetAllAsync();
        Assert.Empty(all);
    }

    [Fact]
    public async Task AddAsync_WithNegativeDuration_ReturnsValidationFailure()
    {
        var dto = ValidDto() with { DurationSeconds = -1 };

        var result = await _srvc.AddAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "DURATION_NEGATIVE");
    }

    [Fact]
    public async Task GetByIdAsync_WhenNotFound_ReturnsNotFoundResult()
    {
        var result = await _srvc.GetByIdAsync(999);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultErrorType.NotFound, result.ErrorType);
    }

    [Fact]
    public async Task GetByIdAsync_WhenFound_ReturnsActivity()
    {
        var created = await _srvc.AddAsync(ValidDto());
        var result = await _srvc.GetByIdAsync(created.Value!.Id!.Value);

        Assert.True(result.IsSuccess);
        Assert.Equal("dupont", result.Value!.Name);
    }

    [Fact]
    public async Task DeleteAsync_WhenFound_RemovesFromRepository()
    {
        var created = await _srvc.AddAsync(ValidDto());
        var result = await _srvc.DeleteAsync(created.Value!.Id!.Value);
        var all = await _repo.GetAllAsync();

        Assert.True(result.IsSuccess);
        Assert.Empty(all);
    }

    [Fact]
    public async Task DeleteAsync_WhenNotFound_ReturnsNotFoundResult()
    {
        var result = await _srvc.DeleteAsync(999);

        Assert.Equal(ResultErrorType.NotFound, result.ErrorType);
    }
}