using Kalmia.Core.Entities;
using Kalmia.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kalmia.Data.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly KalmiaDbContext _dbContext;
    public ActivityRepository(KalmiaDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Activity>> GetAllAsync()
    {
        return await _dbContext.Activities
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Activity?> GetByIdAsync(int id)
    {
        return await _dbContext.Activities
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Activity> AddAsync(Activity activity)
    {
        _dbContext.Activities.Add(activity);
        await _dbContext.SaveChangesAsync();
        return activity;
    }

    public async Task UpdateAsync(Activity activity)
    {
        _dbContext.Activities.Update(activity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _dbContext.Activities
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }
}