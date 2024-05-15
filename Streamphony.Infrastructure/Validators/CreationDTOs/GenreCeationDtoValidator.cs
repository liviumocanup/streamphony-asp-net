using FluentValidation;
using Streamphony.Application.App.Genres.Responses;

namespace Streamphony.Infrastructure.Validators.CreationDTOs;

public class GenreCreationDtoValidator : AbstractValidator<GenreCreationDto>
{
    public GenreCreationDtoValidator()
    {
        RuleFor(genre => genre.Name)
            .NotEmpty()
            .MaximumLength(50)
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Genre name must only contain letters and spaces.");

        RuleFor(genre => genre.Description)
            .NotEmpty()
            .MaximumLength(1000);
    }
}
