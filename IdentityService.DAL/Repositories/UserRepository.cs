using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<User> GetAll()
    {
        return _dbContext.Set<User>();
    }

    public async Task AddByEntityAsync(User entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void RemoveByEntity(User entity)
    {
        _dbContext.Remove(entity);
    }

    public void UpdateByEntity(User entity)
    {
        _dbContext.Entry(entity).Property(x => x.Email).IsModified = false;
        _dbContext.Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void RemoveUserById(long userId)
    {
        var user = new User() { Id = userId };
        _dbContext.Attach(user);
        _dbContext.Entry(user).State = EntityState.Deleted;
        _dbContext.Remove(user);
    }

    public void UpdateByEntityAttach(User user)
    {
        _dbContext.Attach(user);
        _dbContext.Entry(user).State = EntityState.Modified;
        _dbContext.Update(user);
    }
}