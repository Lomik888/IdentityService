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
    private readonly IRefreshTokenRepository<RefreshToken> _refreshTokenRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public JwtTokenService(IRefreshTokenRepository<RefreshToken> refreshTokenRepository, IJwtGenerator jwtGenerator)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<UpdateTokensResult> UpdateJwtTokens(string refreshToken)
    {
        var refreshTokenData = await _refreshTokenRepository.GetAll()
            .Where(x => x.Token == refreshToken && x.IsActive == true && x.Expires > DateTime.UtcNow)
            .Select(x => new { x.UserId })
            .SingleOrDefaultAsync();

        if (refreshTokenData == null)
        {
            return new UpdateTokensResult()
            {
                ErrorMessage = ErrorMessage.InvalidRefreshToken,
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }

        var newRefreshToken = _jwtGenerator.GetRefreshToken();
        newRefreshToken.UserId = refreshTokenData.UserId;

        var newAccessToken = _jwtGenerator.GetAccessToken(refreshTokenData.UserId);
        
        await _refreshTokenRepository.AddByEntityAsync(newRefreshToken);
        _refreshTokenRepository.UpdateByEntityAttach(new RefreshToken()
        {
            Id = newRefreshToken.Id,
            IsActive = false
        });
        await _refreshTokenRepository.SaveChangesAsync();

        return new UpdateTokensResult()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            StatusCode = StatusCodes.Status200OK
        };
    }
}