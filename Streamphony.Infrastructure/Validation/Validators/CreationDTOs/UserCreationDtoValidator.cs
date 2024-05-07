using FluentValidation;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Infrastructure.Extensions;

namespace Streamphony.Infrastructure.Validation.Validators.CreationDTOs;

public class UserCreationDtoValidator : AbstractValidator<UserCreationDto>
{
    public UserCreationDtoValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(user => user.Email)
            .NotEmpty()
            .MaximumLength(100)
            .EmailAddress();

        RuleFor(user => user.ArtistName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(user => user.DateOfBirth)
            .DateNotInFuture();

        RuleFor(user => user.ProfilePictureUrl)
            .MaximumLength(1000)
            .ValidUrl();
    }
}
