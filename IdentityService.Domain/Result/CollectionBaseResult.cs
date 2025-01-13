namespace IdentityService.Domain.Result;

public class CollectionBaseResult<T> : DataBaseResult<T>
{
    public long Count { get; set; }
}