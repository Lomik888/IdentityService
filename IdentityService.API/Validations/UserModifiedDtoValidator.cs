using FluentValidation;
using IdentityService.Domain.Dto.UserDto;

namespace IdentityService.API.Validations;

public class UserModifiedDtoValidator : AbstractValidator<UserModifiedDto>
{
    public UserModifiedDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50).WithMessage("FirstName is required");
        RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(50).WithMessage("LastName is required");
    }
}