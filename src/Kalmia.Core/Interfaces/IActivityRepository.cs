using Kalmia.Core.Entities;

namespace Kalmia.Core.Interfaces;

public interface IActivityRepository
{
    Task<List<Activity>> GetAllAsync();
    Task<Activity?> GetByIdAsync(int id);
    Task<Activity> AddAsync(Activity activity);
    Task UpdateAsync(Activity activity);
    Task DeleteAsync(int id);
}