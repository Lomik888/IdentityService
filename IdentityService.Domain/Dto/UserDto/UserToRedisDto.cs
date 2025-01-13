namespace IdentityService.Domain.Dto.UserDto;

public record UserToRedisDto(long Id, string FirstName, string LastName);