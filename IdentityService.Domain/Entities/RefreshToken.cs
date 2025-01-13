using IdentityService.Domain.Interfaces;

namespace IdentityService.Domain.Entities;

public class RefreshToken : IEntityId<long>
{
    public long Id { get; set; }
    
    public string Token { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTime Expires { get; set; }
    
    public long UserId { get; set; }
    
    public User User { get; set; }
}