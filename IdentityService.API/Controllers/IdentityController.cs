using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using IdentityService.Domain.Result.UserResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[AllowAnonymous]
[Route("/api/[controller]/")]
[ApiController]
public class IdentityController : ControllerBase
{
    #region DI and ctor

    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    #endregion

    [HttpPost("registration")]
    public async Task<ActionResult<BaseResult>> RegistrationAsync([FromBody] UserRegistrationDto userRegistrationDto)
    {
        var resonse = await _identityService.RegistrationUserByRegistrationDtoAsync(userRegistrationDto);

        if (resonse.IsSuccess)
        {
            return Ok(resonse);
        }

        return BadRequest(resonse);
    }

    [HttpPost("login")]
    public async Task<ActionResult<DataBaseResult<LoginResult>>> LoginAsync([FromBody] UserLoginDto userLoginDto)
    {
        var resonse = await _identityService.LoginUserAsync(userLoginDto.Email, userLoginDto.Password);

        if (resonse.IsSuccess)
        {
            return Ok(resonse);
        }

        return BadRequest(resonse);
    }
}