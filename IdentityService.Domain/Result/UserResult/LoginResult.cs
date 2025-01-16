namespace IdentityService.Domain.Result.UserResult;

public class LoginResult : BaseResult
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}