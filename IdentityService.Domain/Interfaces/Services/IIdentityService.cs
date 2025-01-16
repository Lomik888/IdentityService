using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Result;
using IdentityService.Domain.Result.UserResult;

namespace IdentityService.Domain.Interfaces.Services;

public interface IIdentityService
{
    Task<BaseResult> RegistrationUserByRegistrationDtoAsync(UserRegistrationDto userRegistrationDto);

    Task<DataBaseResult<LoginResult>> LoginUserAsync(string email, string password);
}