using FluentValidation;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Infrastructure.Extensions;
using Streamphony.Infrastructure.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validators.DTOs;

public class SongDtoValidator : AbstractValidator<SongDto>
{
    public SongDtoValidator()
    {
        RuleFor(song => song.Duration)
            .Must(duration => duration > TimeSpan.Zero)
            .WithMessage("Duration must be greater than zero.");

        RuleFor(song => song.CoverUrl)
            .NotEmpty()
            .MaximumLength(1000)
            .ValidUrl();
        
        RuleFor(song => song.AudioUrl)
            .NotEmpty()
            .MaximumLength(1000)
            .ValidUrl();
    }
}
