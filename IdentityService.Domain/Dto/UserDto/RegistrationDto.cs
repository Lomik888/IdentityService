using IdentityService.Domain.Interfaces.Services;

namespace IdentityService.Domain.Dto.UserDto;

public record RegistrationDto(string FirstName, string LastName, string Email, string Password);