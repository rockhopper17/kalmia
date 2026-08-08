using Kalmia.Core.Common;
using Kalmia.Core.Entities;

namespace Kalmia.Core.Interfaces;

public interface IUserProfileService
{
    Task<Result<List<UserProfileDto>>> GetAllAsync();
    Task<Result<UserProfileDto>> GetByIdAsync(int id);
    Task<Result<UserProfileDto>> AddAsync(UserProfileDto dto);
    Task<Result<UserProfileDto>> UpdateAsync(int id, UserProfileDto dto);
    Task<Result<Unit>> DeleteAsync(int id);
}