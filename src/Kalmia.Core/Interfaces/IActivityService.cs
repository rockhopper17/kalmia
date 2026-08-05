using Kalmia.Core.Entities;

namespace Kalmia.Core.Interfaces;

public interface IActivityService
{
    Task<List<ActivityDto>> GetAllAsync();
    Task<ActivityDto?> GetByIdAsync(int id);
    Task<ActivityDto> AddAsync(ActivityDto dto);
    Task<bool> UpdateAsync(int id, ActivityDto dto);
    Task<bool> DeleteAsync(int id);
}