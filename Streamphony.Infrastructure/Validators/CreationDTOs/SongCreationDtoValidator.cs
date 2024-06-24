using FluentValidation;
using Streamphony.Application.App.Songs.DTOs;

namespace Streamphony.Infrastructure.Validators.CreationDTOs;

public class SongCreationDtoValidator : AbstractValidator<SongCreationDto>
{
    public SongCreationDtoValidator()
    {
        RuleFor(song => song.Title)
            .NotEmpty()
            .MaximumLength(50);
    }
}
