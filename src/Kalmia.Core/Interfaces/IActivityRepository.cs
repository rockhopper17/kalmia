using Kalmia.Core.Entities;

namespace Kalmia.Core.Interfaces;

public interface IActivityRepository
{
    Task<List<Activity>> GetAllAsync();
    Task<Activity?> GetByIdAsync(int id);
    Task<Activity> AddAsync(Activity entity);
    Task UpdateAsync(Activity entity);
    Task DeleteAsync(int id);
}