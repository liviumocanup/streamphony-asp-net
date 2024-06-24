using FluentValidation;
using Streamphony.Application.App.Auth.Responses;

namespace Streamphony.Infrastructure.Validators.CreationDTOs;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);

        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(user => user.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(user => user.LastName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
