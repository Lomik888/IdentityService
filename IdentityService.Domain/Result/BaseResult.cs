namespace IdentityService.Domain.Result;

public class BaseResult
{
    public bool IsSuccess => ErrorMessage == null;
    
    public string ErrorMessage { get; set; }
    
    public int StatusCode { get; set; }
}