namespace IdentityService.Domain.Interfaces.Extensions;

public interface IPasswordHasher
{
    Task<string> HashPasswordAsync(string password, byte[] salt = null);

    Task<bool> VerifyPasswordAsync(string password, string hash);
}