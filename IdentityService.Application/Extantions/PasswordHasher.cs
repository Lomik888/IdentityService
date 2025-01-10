using System.Security.Cryptography;
using System.Text;
using DotNetEnv;
using IdentityService.Domain.Interfaces.Extantions;
using Konscious.Security.Cryptography;

namespace IdentityService.Application.Extantions;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltLength = 16;
    private const int HashLength = 32;
    private const int Iterations = 3;
    private const int MemorySize = 65536;
    private const int DegreeOfParallelism = 2;

    public async Task<string> HashPasswordAsync(string password, byte[] salt = null)
    {
        salt ??= GenerateSalt();

        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            argon2.Salt = salt;
            argon2.MemorySize = MemorySize;
            argon2.DegreeOfParallelism = DegreeOfParallelism;
            argon2.Iterations = Iterations;
            argon2.KnownSecret = Encoding.UTF8.GetBytes(Env.GetString("HASH_SECRET_KEY"));

            var hash = await argon2.GetBytesAsync(HashLength);
            return $"{Convert.ToBase64String(hash)}:{Convert.ToBase64String(salt)}";
        }
    }

    public async Task<bool> VerifyPasswordAsync(string password, string hash)
    {
        var salt = Encoding.UTF8.GetBytes(hash.Split(':').First());
        
        var passwordHash =  await HashPasswordAsync(password, salt);

        return hash == passwordHash;
    }

    private byte[] GenerateSalt()
    {
        var salt = new byte[SaltLength];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return salt;
    }
}