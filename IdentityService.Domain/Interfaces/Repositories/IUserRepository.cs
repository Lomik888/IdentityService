using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    void RemoveUserById(long userId);

    void UpdateByEntityAttach(User user);
}