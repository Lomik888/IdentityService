using IdentityService.Domain.Interfaces;

namespace IdentityService.Domain.Entities;

public class Password : IEntityId<long>
{
    public long Id { get; set; }

    public string PasswordHash { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }
}