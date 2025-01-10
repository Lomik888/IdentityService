namespace IdentityService.Domain.Interfaces.Extantions;

public interface IJwtGenerator
{
    string GetAccessTokenAsync(string userId);

    string GetRefreshTokenAsync();
}