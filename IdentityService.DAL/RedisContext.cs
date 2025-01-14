using DotNetEnv;
using StackExchange.Redis;

namespace IdentityService.DAL;

public class RedisContext
{
    private readonly string _connectionString;
    
    public RedisContext()
    {
        _connectionString = Env.GetString("REDIS_URL");
    }
    
    public IConnectionMultiplexer CreateConnection()
    {
        return ConnectionMultiplexer.Connect(_connectionString);
    }
}