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

    public async Task<List<ActivityDto>> GetAllAsync()
    {
        var activities = await _repo.GetAllAsync();
        return activities.Select(a => a.ToDto()).ToList();
    }

    public async Task<ActivityDto?> GetByIdAsync(int id)
    {
        var activity = await _repo.GetByIdAsync(id);
        return activity?.ToDto();
    }

    public async Task<ActivityDto> AddAsync(ActivityDto dto)
    {
        var activity = dto.ToEntity();
        var created = await _repo.AddAsync(activity);
        return created.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, ActivityDto dto)
    {
        var activity = await _repo.GetByIdAsync(id);
        if (activity is null) return false;
        activity.SetFieldsFrom(dto);
        await _repo.UpdateAsync(activity);
        return true;
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var activity = await _repo.GetByIdAsync(id);
        if (activity is null) return false;
        await _repo.DeleteAsync(id);
        return true;
    }
}