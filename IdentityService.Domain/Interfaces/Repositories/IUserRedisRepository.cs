using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces.Repositories;

public interface IUserRedisRepository
{
    void AddUsersAsync(UserDto user);
}