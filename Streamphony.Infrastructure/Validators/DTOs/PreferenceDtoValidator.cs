using FluentValidation;
using Streamphony.Application.App.Preferences.Responses;

namespace Streamphony.Infrastructure.Validators.DTOs;

public class PreferenceDtoValidator : AbstractValidator<PreferenceDto>
{
    public PreferenceDtoValidator()
    {
        RuleFor(preference => preference.Language)
            .MaximumLength(2);
    }
}
