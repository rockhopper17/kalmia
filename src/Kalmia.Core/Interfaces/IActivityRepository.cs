using Kalmia.Core.Entities;

namespace Kalmia.Core.Interfaces;

public interface IActivityRepository
{
    Task<Activity?> GetByIdAsync(int id);
    Task<List<Activity>> GetAllAsync();
    Task<Activity> AddAsync(Activity activity);
    Task UpdateAsync(Activity activity);
    Task DeleteAsync(int id);
}