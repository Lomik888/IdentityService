using IdentityService.Domain.Interfaces.Repositories.AccessTokenRepository;

namespace IdentityService.DAL.Repositories.RedisRepositories;

public class AccessTokenBlackListRedisRepository : IAccessTokenBlackListRedisRepository
{
    private readonly RedisContext _redisContext;

    public AccessTokenBlackListRedisRepository(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    public async Task AddTokenToBlackListAsync(string tokenId, long expireTime)
    {
        using (var connect = _redisContext.CreateConnection())
        {
            var db = connect.GetDatabase(1);
            await db.StringSetAsync($"token:{tokenId}", string.Empty, TimeSpan.FromSeconds(expireTime));
        }
    }
}