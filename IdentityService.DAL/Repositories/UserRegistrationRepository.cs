using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;

namespace IdentityService.DAL.Repositories;

public class UserRegistrationRepository : IRegistrationRepository<User>
{
    private readonly ApplicationDbContext _dbContext;

    public UserRegistrationRepository(ApplicationDbContext dbContext)
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

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}