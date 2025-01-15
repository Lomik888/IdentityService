using IdentityService.Domain.Interfaces.Repositories;

namespace IdentityService.DAL.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task AddByEntityAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void RemoveByEntity(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public void UpdateByEntity(TEntity entity)
    {
        _dbContext.Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}