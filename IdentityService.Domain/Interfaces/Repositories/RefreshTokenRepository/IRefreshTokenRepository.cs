namespace IdentityService.Domain.Interfaces.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository<TEntity>
{
    IQueryable<TEntity> GetAll();

    Task AddByEntityAsync(TEntity entity);

    Task DapperUpdateRefreshTokenActive(long refreshTokenId, bool isActive);

    Task SaveChangesAsync();
}