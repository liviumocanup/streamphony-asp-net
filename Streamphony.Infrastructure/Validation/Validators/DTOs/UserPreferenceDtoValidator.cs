using FluentValidation;
using Streamphony.Application.App.UserPreferences.Responses;

namespace Streamphony.Infrastructure.Validation.Validators.DTOs;

public class UserPreferenceDtoValidator : AbstractValidator<UserPreferenceDto>
{
    public UserPreferenceDtoValidator()
    {
        RuleFor(userPreference => userPreference.Language)
            .MaximumLength(2);
    }
}
