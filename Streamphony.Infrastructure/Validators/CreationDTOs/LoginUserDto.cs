using FluentValidation;
using Streamphony.Application.App.Auth.Responses;

namespace Streamphony.Infrastructure.Validators.CreationDTOs;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(user => user.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);
    }
}
