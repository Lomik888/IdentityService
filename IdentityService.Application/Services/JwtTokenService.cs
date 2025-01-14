using IdentityService.Application.Resources;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Extensions;
using IdentityService.Domain.Interfaces.Repositories;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result.TokenResult;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Services;

public class JwtTokenService : IJwtTokenService
{
    #region DI and ctor

    private readonly IRefreshTokenRepository<RefreshToken> _refreshTokenRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public JwtTokenService(IRefreshTokenRepository<RefreshToken> refreshTokenRepository, IJwtGenerator jwtGenerator)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtGenerator = jwtGenerator;
    }

    #endregion

    public async Task<UpdateTokensResult> UpdateJwtTokensAsync(string accessToken)
    {
        var userId = Convert.ToInt64(_jwtGenerator.GetIdFromAccessToken(accessToken));

        var refreshTokenId = await _refreshTokenRepository.GetAll()
            .Where(x => x.UserId == userId && x.IsActive == true && x.Expires > DateTime.UtcNow)
            .Select(x => new { x.Id })
            .SingleOrDefaultAsync();

        if (refreshTokenId == null)
        {
            return new UpdateTokensResult()
            {
                ErrorMessage = ErrorMessage.InvalidRefreshToken,
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }

        var newRefreshToken = _jwtGenerator.GetRefreshToken();
        newRefreshToken.UserId = userId;

        var newAccessToken = _jwtGenerator.GetAccessToken(userId);

        await _refreshTokenRepository.UpdateRefreshTokenActive(refreshTokenId.Id, false);
        await _refreshTokenRepository.AddByEntityAsync(newRefreshToken);
        await _refreshTokenRepository.SaveChangesAsync();

        return new UpdateTokensResult()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            StatusCode = StatusCodes.Status200OK
        };
    }
}