using FluentValidation;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Infrastructure.Extensions;

namespace Streamphony.Infrastructure.Validation.Validators.CreationDTOs;

public class SongCreationDtoValidator : AbstractValidator<SongCreationDto>
{
    public SongCreationDtoValidator()
    {
        RuleFor(song => song.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(song => song.Duration)
            .Must(duration => duration > TimeSpan.Zero)
            .WithMessage("Duration must be greater than zero.");

        RuleFor(song => song.Url)
            .NotEmpty()
            .MaximumLength(1000)
            .ValidUrl();
    }
}
