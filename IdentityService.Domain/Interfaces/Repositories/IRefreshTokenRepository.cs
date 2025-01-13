namespace IdentityService.Domain.Interfaces.Repositories;

public interface IRefreshTokenRepository<TEntity>
{
    IQueryable<TEntity> GetAll();
    
    Task AddByEntityAsync(TEntity entity);
    
    void UpdateByEntityAttach(TEntity entity);
    
    Task SaveChangesAsync();
}