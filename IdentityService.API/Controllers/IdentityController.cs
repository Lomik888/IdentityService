using System.Security.Principal;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[Route("api/")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("registration")]
    public async Task<ActionResult<BaseResult>> RegistrationByDto([FromBody]RegistrationDto registerDto)
    {
        return Ok(await _identityService.RegistrationUserAsync(registerDto));
    }
}