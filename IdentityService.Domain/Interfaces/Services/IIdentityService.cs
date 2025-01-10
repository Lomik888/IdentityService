using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Result;

namespace IdentityService.Domain.Interfaces.Services;

public interface IIdentityService
{
    Task<BaseResult> RegistrationUserAsync(RegistrationDto registrationDto);
    
    Task<DataBaseResult<LoginResult>> LoginUserAsync(LoginDto loginDto, string accessToken);
}