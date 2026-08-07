using Kalmia.Core.Entities;
using Kalmia.Core.Interfaces;

namespace Kalmia.Core.Tests.Fakes;

// mock repository so unit tests can make calls here without needing a database
public class FakeActivityRepository : IActivityRepository
{
    private readonly List<Activity> _activities = new();
    private int _nextId = 1;

    public Task<List<Activity>> GetAllAsync()
    {
        return Task.FromResult(_activities.ToList());
    }

    public Task<Activity?> GetByIdAsync(int id)
    {
        return Task.FromResult(_activities.FirstOrDefault(a => a.Id == id));
    }

    public Task<Activity> AddAsync(Activity activity)
    {
        activity.Id = _nextId++;
        _activities.Add(activity);
        return Task.FromResult(activity);
    }
    
    public Task UpdateAsync(Activity activity)
    {
        var index = _activities.FindIndex(a => a.Id == activity.Id);
        if (index >= 0) _activities[index] = activity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        _activities.RemoveAll(a => a.Id == id);
        return Task.CompletedTask;
    }
}