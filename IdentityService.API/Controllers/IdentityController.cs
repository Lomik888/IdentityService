using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[AllowAnonymous]
[Route("/api/[controller]/")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("registration")]
    public async Task<ActionResult<BaseResult>> Registration([FromBody] UserRegistrationDto userRegistrationDto)
    {
        return Ok(await _identityService.RegistrationUserByRegistrationDtoAsync(userRegistrationDto));
    }

    [HttpPost("login")]
    public async Task<ActionResult<DataBaseResult<LoginResult>>> Login([FromBody] UserLoginDto userLoginDto)
    {
        return Ok(await _identityService.LoginUserAsync(userLoginDto.Email, userLoginDto.Password));
    }
}