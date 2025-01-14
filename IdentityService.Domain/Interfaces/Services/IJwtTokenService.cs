using IdentityService.Domain.Result.TokenResult;

namespace IdentityService.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    Task<UpdateTokensResult> UpdateJwtTokensAsync(string accessToken);
}