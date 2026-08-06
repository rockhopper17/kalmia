using Kalmia.Core.Common;
using Kalmia.Core.Entities;

namespace Kalmia.Core.Interfaces;

public interface IActivityService
{
    Task<Result<List<ActivityDto>>> GetAllAsync();
    Task<Result<ActivityDto>> GetByIdAsync(int id);
    Task<Result<ActivityDto>> AddAsync(ActivityDto dto);
    Task<Result<ActivityDto>> UpdateAsync(int id, ActivityDto dto);
    Task<Result<Unit>> DeleteAsync(int id);
}