using FluentValidation;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Infrastructure.Validation.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validation.Validators.DTOs;

public class GenreDtoValidator : AbstractValidator<GenreDto>
{
    public GenreDtoValidator()
    {
        Include(new GenreCreationDtoValidator());
    }
}
