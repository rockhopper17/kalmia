using Kalmia.Core.Entities;

namespace Kalmia.Core.Interfaces;

public interface IUserProfileRepository
{
    Task<List<UserProfile>> GetAllAsync();
    Task<UserProfile?> GetByIdAsync(int id);
    Task<UserProfile> AddAsync(UserProfile entity);
    Task UpdateAsync(UserProfile entity);
    Task DeleteAsync(int id);
}