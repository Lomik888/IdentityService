using System.Text.Json;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Interfaces.Repositories.UserRepository;

namespace IdentityService.DAL.Repositories.RedisRepositories;

public class UserRedisRepository : IUserRedisRepository
{
    private readonly RedisContext _redisContext;

    public UserRedisRepository(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    public async Task AddUsersAsync(UserToRedisDto user)
    {
        using (var connect = _redisContext.CreateConnection())
        {
            var db = connect.GetDatabase(0);
            await db.StringAppendAsync($"user:{user.Id}", JsonSerializer.SerializeToUtf8Bytes(user));
        }
    }

    public async Task ResetUsersAsync()
    {
        using (var connect = _redisContext.CreateConnection(true))
        {
            var db = connect.GetServers()[0];
            await db.FlushDatabaseAsync();
        }
    }
}