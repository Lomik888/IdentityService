namespace IdentityService.Domain.Interfaces.Repositories;

public interface IRegistrationRepository<TEntity>
{
    IQueryable<TEntity> GetAll();
    
    Task AddByEntityAsync(TEntity entity);
    
    Task SaveChangesAsync();
}