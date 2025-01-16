namespace IdentityService.Domain.Result;

public class DataBaseResult<T> : BaseResult
{
    public T Data { get; set; }
}