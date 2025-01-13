using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Result;

namespace IdentityService.Domain.Interfaces.Services;

public interface IUserService
{
    Task<CollectionBaseResult<List<UserDto>>> GetAllUsersAsync();

    Task<BaseResult> RemoveUserByUserIdAsync(long userId);

    Task<BaseResult> ModifiedUserAsync(UserModifiedDto userModifiedDto, long userId);
}