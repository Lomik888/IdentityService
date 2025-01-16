using FluentValidation;
using IdentityService.Domain.Dto.UserDto;

namespace IdentityService.API.Validations;

public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
{
    public UserRegistrationDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50).WithMessage("FirstName is required");
        RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(50).WithMessage("LastName is required");
        RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().MaximumLength(50).WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(5).MaximumLength(15)
            .WithMessage("Password is required");
    }
}