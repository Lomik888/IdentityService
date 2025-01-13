namespace IdentityService.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
{
    IQueryable<TEntity> GetAll();

    Task AddByEntityAsync(TEntity entity);

    void RemoveByEntity(TEntity entity);

    void UpdateByEntity(TEntity entity);

    Task SaveChangesAsync();
}