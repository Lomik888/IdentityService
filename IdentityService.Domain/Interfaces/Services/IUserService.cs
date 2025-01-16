using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Result;

namespace IdentityService.Domain.Interfaces.Services;

public interface IUserService
{
    Task<CollectionBaseResult<List<UserDto>>> GetAllUsersAsync();

    Task<BaseResult> RemoveUserByUserIdAsync(string accessToken);

    Task<BaseResult> ModifiedUserAsync(UserModifiedDto userModifiedDto, string accessToken);
}