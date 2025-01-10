using IdentityService.Domain.Interfaces;

namespace IdentityService.Domain.Entities;

public class Password : IEntityId<ulong>
{
    public ulong Id { get; set; }
    
    public string PasswordHash { get; set; }
    
    public ulong UserId { get; set; }
    
    public User User { get; set; }
}