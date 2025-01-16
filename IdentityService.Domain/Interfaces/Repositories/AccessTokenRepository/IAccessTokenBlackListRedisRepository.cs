namespace IdentityService.Domain.Interfaces.Repositories.AccessTokenRepository;

public interface IAccessTokenBlackListRedisRepository
{
    Task AddTokenToBlackListAsync(string tokenId, long expireTime);
}