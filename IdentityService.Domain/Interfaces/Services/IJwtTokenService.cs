using IdentityService.Domain.Result.TokenResult;

namespace IdentityService.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    Task<UpdateTokensResult> UpdateJwtTokens(string accessToken);
}