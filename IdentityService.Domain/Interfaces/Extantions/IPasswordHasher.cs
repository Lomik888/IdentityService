namespace IdentityService.Domain.Interfaces.Extantions;

public interface IPasswordHasher
{
    Task<string> HashPasswordAsync(string password, byte[] salt = null);
    
    Task<bool> VerifyPasswordAsync(string password, string hash);
}