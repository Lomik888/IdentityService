using System.Security.Claims;
using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces.Extensions;

public interface IJwtGenerator
{
    string GetAccessToken(long userId);

    RefreshToken GetRefreshToken();

    public IEnumerable<Claim> GetClaimsFromAccessToken(string accessToken);

    public string GetIdFromAccessToken(string accessToken);

    long GetExpireTimeSecondsAccessToken(string accessToken);
}