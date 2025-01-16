namespace IdentityService.Domain.Result.TokenResult;

public class UpdateTokensResult : BaseResult
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}