using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace IdentityService.Domain.Extantions;

public static class DistributedCacheExtensions
{
    public static async Task<T?> GetObject<T>(this IDistributedCache cache, string key)
    {
        var data = await cache.GetAsync(key);
        return data?.Length > 0 ? JsonSerializer.Deserialize<T>(data) : default(T);
    }

    public static async Task SetObjects<T>(this IDistributedCache cache, Dictionary<string, T> objects,
        DistributedCacheEntryOptions options = null)
    {
        foreach (var o in objects)
        {
            var key = o.Key;
            var value = JsonSerializer.SerializeToUtf8Bytes(o.Value);

            if (value.Length > 0)
            {
                await cache.SetAsync(key, value, options ?? new DistributedCacheEntryOptions());
            }
        }
    }
}