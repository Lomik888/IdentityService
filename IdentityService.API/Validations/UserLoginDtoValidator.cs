using FluentValidation;
using IdentityService.Domain.Dto.UserDto;

namespace IdentityService.API.Validations;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().MaximumLength(50).WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(5).MaximumLength(15)
            .WithMessage("Password is required");
    }
}