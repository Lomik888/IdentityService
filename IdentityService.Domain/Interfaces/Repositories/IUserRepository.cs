namespace IdentityService.Domain.Interfaces.Repositories;

public interface IUserRepository<TEntity>
{
    IQueryable<TEntity> GetAll();

    Task AddByEntityAsync(TEntity entity);

    Task SaveChangesAsync();

    Task RemoveUserByIdAsync(long userId);

    Task UpdateByEntityAsync(TEntity user);
}