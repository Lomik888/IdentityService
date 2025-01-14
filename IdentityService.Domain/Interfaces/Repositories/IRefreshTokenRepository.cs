namespace IdentityService.Domain.Interfaces.Repositories;

public interface IRefreshTokenRepository<TEntity>
{
    IQueryable<TEntity> GetAll();

    Task AddByEntityAsync(TEntity entity);

    Task UpdateRefreshTokenActive(long refreshTokenId, bool isActive);

    Task SaveChangesAsync();
}