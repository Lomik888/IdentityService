using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result.TokenResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[Authorize]
[Route("/api/[controller]/")]
[ApiController]
public class TokenController : ControllerBase
{
    #region DI ctor

    private readonly IJwtTokenService _jwtTokenService;

    public TokenController(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    #endregion


    [HttpGet("accessToken")]
    public async Task<ActionResult<UpdateTokensResult>> RefreshAccessTokenAsync()
    {
        var response =
            await _jwtTokenService.UpdateJwtTokensAsync(HttpContext.Request.Headers.Authorization.ToString());

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}