namespace IdentityService.Domain.Interfaces.Services;

public interface IRedisService
{
    Task ClearRedisCacheAsync();

    Task AddObjectsToRedisCacheAsync<T>(Dictionary<string, T> objects);
}