using IdentityService.Domain.Extantions;
using IdentityService.Domain.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace IdentityService.Application.Services;

public class RedisService 
{
    // private readonly IDistributedCache _cache;
    // private readonly ConnectionMultiplexer _connectionMultiplexer;
    //
    // public RedisService(ConnectionMultiplexer connectionMultiplexer, IDistributedCache cache)
    // {
    //     _connectionMultiplexer = connectionMultiplexer;
    //     _cache = cache;
    // }
    //
    // public async Task ClearRedisCacheAsync()
    // {
    //     var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints()[0]);
    //     await server.FlushDatabaseAsync();
    // }
    //
    // public async Task AddObjectsToRedisCacheAsync<T>(Dictionary<string, T> objects)
    // {
    //    await _cache.SetObjects(objects);
    // }
    //
    // public async Task<T?> GetObjectFromRedisCacheAsync<T>(string key)
    // {
    //     return await _cache.GetObject<T>(key);
    // }
}