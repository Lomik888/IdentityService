namespace IdentityService.Domain.Result;

public class LoginResult : BaseResult
{
    public string AccessToken { get; set; }
    
    public string RefreshToken { get; set; }
}