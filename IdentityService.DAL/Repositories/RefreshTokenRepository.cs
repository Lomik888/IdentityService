using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository<RefreshToken>
{
    private readonly ApplicationDbContext _dbContext;

    public RefreshTokenRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<RefreshToken> GetAll()
    {
        return _dbContext.Set<RefreshToken>();
    }

    public async Task AddByEntityAsync(RefreshToken entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void UpdateByEntityAttach(RefreshToken entity)
    {
        _dbContext.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}