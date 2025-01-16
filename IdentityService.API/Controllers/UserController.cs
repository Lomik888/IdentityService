using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[Authorize]
[Route("/api/[controller]/")]
[ApiController]
public class UserController : ControllerBase
{
    #region DI and ctor

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    [AllowAnonymous]
    [HttpGet("users")]
    public async Task<ActionResult<CollectionBaseResult<List<UserDto>>>> GetAllUsersAsync()
    {
        var response = await _userService.GetAllUsersAsync();

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("update")]
    public async Task<ActionResult<BaseResult>> UpdateUserDataAsync([FromBody] UserModifiedDto userModifiedDto)
    {
        var response = await _userService.ModifiedUserAsync(userModifiedDto,
            HttpContext.Request.Headers.Authorization.ToString());

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<CollectionBaseResult<List<UserDto>>>> DeletUserAsync()
    {
        var response = await _userService.RemoveUserByUserIdAsync(HttpContext.Request.Headers.Authorization.ToString());

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}