namespace IdentityService.Domain.Interfaces.Repositories.UserRepository;

public interface IUserRepository<TEntity>
{
    IQueryable<TEntity> GetAll();

    Task AddByEntityAsync(TEntity entity);

    Task SaveChangesAsync();

    Task DapperRemoveUserByIdAsync(long userId);

    Task DapperUpdateByEntityAsync(TEntity user);

    Task EFCoreUpdateByEntityAsync(TEntity user);
}