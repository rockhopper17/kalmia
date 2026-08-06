using Kalmia.Core.Common;
using Kalmia.Core.Entities;
using Kalmia.Core.Interfaces;

namespace Kalmia.Core.Services;

// ------------------------------------------------------------------------------------------------
// Implementation of IActivityService to contain Activity business logic
// - constructed via dependency injection in the api layer which supplies the concrete 
//   IActivityRepository implementation from the data layer.
// ------------------------------------------------------------------------------------------------
public class ActivityService : IActivityService
{
    // repository instance that will be dependency injected via calling code (data layer)
    private readonly IActivityRepository _repo;
    public ActivityService(IActivityRepository repo) => _repo = repo;

    public async Task<Result<List<ActivityDto>>> GetAllAsync()
    {
        var activities = await _repo.GetAllAsync();
        return Result<List<ActivityDto>>.Success(activities.Select(a => a.ToDto()).ToList());
    }

    public async Task<Result<ActivityDto>> GetByIdAsync(int id)
    {
        var activity = await _repo.GetByIdAsync(id);
        return activity is null ? Result<ActivityDto>.NotFound() : Result<ActivityDto>.Success(activity.ToDto());
    }

    public async Task<Result<ActivityDto>> AddAsync(ActivityDto dto)
    {
        var validation = ValidateActivity(dto);
        if (!validation.IsSuccess)
            return Result<ActivityDto>.Failure(validation.Errors);

        var activity = dto.ToEntity();
        var created = await _repo.AddAsync(activity);
        return Result<ActivityDto>.Success(created.ToDto());
    }

    public async Task<Result<ActivityDto>> UpdateAsync(int id, ActivityDto dto)
    {
        var validation = ValidateActivity(dto);
        if (!validation.IsSuccess)
            return Result<ActivityDto>.Failure(validation.Errors);

        var activity = await _repo.GetByIdAsync(id);
        if (activity is null)
            return Result<ActivityDto>.NotFound();

        activity.SetFieldsFrom(dto);
        await _repo.UpdateAsync(activity);
        return Result<ActivityDto>.Success(activity.ToDto());
    }
    
    public async Task<Result<Unit>> DeleteAsync(int id)
    {
        var activity = await _repo.GetByIdAsync(id);
        if (activity is null)
            return Result<Unit>.NotFound();

        await _repo.DeleteAsync(id);
        return Result<Unit>.Success(Unit.Value);
    }

    private static Result<Unit> ValidateActivity(ActivityDto dto)
    {
        var errors = new List<ErrorDetail>();

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add(new ErrorDetail("NAME_REQUIRED", "Name is required.", nameof(dto.Name)));
        else if (dto.Name.Length > 200)
            errors.Add(new ErrorDetail("NAME_TOO_LONG", "Name must be 200 characters or fewer.", nameof(dto.Name)));
        
        if (dto.DurationSeconds < 0)
            errors.Add(new ErrorDetail("DURATION_NEGATIVE", "Duration must be greater than or equal to zero.", nameof(dto.DurationSeconds)));
        
        if (dto.DistanceMeters < 0)
            errors.Add(new ErrorDetail("DISTANCE_NEGATIVE", "Distance must be greater than or equal to zero.", nameof(dto.DistanceMeters)));
        
        if (dto.Description?.Length > 2000)
            errors.Add(new ErrorDetail("DESCRIPTION_TOO_LONG", "Description must be 2000 characters or fewer.", nameof(dto.Description)));

        return errors.Count == 0 ? Result.Success() : Result.Failure(errors);
    }
}