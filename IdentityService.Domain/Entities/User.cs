using IdentityService.Domain.Interfaces;

namespace IdentityService.Domain.Entities;

public class User : IEntityId<long>
{
    public long Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public Password Password { get; set; }
    
    public List<RefreshToken> RefreshTokens { get; set; }
}