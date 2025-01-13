using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces.Extantions;

public interface IJwtGenerator
{
    string GetAccessToken(long userId);

    RefreshToken GetRefreshToken();
}