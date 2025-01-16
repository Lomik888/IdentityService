using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces.Repositories.UserRepository;

public interface IUserRedisRepository
{
    Task AddUsersAsync(UserToRedisDto user);

    Task ResetUsersAsync();
}