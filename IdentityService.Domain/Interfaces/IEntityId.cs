namespace IdentityService.Domain.Interfaces;

public interface IEntityId<T> where T : struct
{
    T Id { get; set; }
}