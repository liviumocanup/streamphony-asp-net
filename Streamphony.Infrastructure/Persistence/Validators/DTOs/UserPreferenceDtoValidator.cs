using FluentValidation;
using Streamphony.Application.App.UserPreferences.Responses;

namespace Streamphony.Infrastructure.Persistence.Validators.DTOs;

public class UserPreferenceDtoValidator : AbstractValidator<UserPreferenceDto>
{
    public UserPreferenceDtoValidator()
    {
        RuleFor(userPreference => userPreference.Language)
            .MaximumLength(2);
    }
}
