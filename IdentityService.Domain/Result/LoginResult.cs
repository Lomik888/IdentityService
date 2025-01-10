namespace IdentityService.Domain.Result;

public record LoginResult(string AccessToken, string RefreshToken);