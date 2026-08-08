using Kalmia.Core.Entities;
using Kalmia.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kalmia.Infrastructure.Data.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly KalmiaDbContext _dbContext;
    public UserProfileRepository(KalmiaDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<UserProfile>> GetAllAsync()
    {
        return await _dbContext.UserProfiles
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserProfile?> GetByIdAsync(int id)
    {
        return await _dbContext.UserProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<UserProfile> AddAsync(UserProfile entity)
    {
        _dbContext.UserProfiles.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(UserProfile entity)
    {
        _dbContext.UserProfiles.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _dbContext.UserProfiles
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}