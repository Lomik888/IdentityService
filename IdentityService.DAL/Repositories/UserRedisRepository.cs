using System.Text.Json;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;

namespace IdentityService.DAL.Repositories;

public class UserRedisRepository : IUserRedisRepository
{
    private readonly RedisContext _redisContext;

    public UserRedisRepository(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    public void AddUsersAsync(UserDto user)
    {
        using (var connect = _redisContext.CreateConnection())
        {
            var db = connect.GetDatabase(0);
            var id = Guid.NewGuid();
            db.SetAdd($"user:{id}", JsonSerializer.SerializeToUtf8Bytes(user));

            var fromredisuser = db.StringGet($"user:{id}");
            
            Console.WriteLine();
        }
    }
}