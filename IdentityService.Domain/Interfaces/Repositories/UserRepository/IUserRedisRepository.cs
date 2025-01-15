using IdentityService.Domain.Dto.UserDto;

namespace IdentityService.Domain.Interfaces.Repositories.UserRepository;

public interface IUserRedisRepository
{
    Task AddUsersAsync(UserToRedisDto user);

    Task ResetUsersAsync();
}