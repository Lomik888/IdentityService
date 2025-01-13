namespace IdentityService.Domain.Dto.UserDto;

public record UserRegistrationDto(string FirstName, string LastName, string Email, string Password);