using IdentityService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[Authorize]
[Route("/api/[controller]/")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IJwtTokenService jwtTokenService;

    public TokenController(IJwtTokenService jwtTokenService)
    {
        this.jwtTokenService = jwtTokenService;
    }
    
    
}