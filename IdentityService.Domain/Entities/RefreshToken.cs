using IdentityService.Domain.Interfaces;

namespace IdentityService.Domain.Entities;

public class RefreshToken : IEntityId<long>
{
    public long Id { get; set; }
    
    public string Token { get; set; }
    
    public ulong UserId { get; set; }
    
    public User User { get; set; }
}