using FluentValidation;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Infrastructure.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validators.DTOs;

public class GenreDtoValidator : AbstractValidator<GenreDto>
{
    public GenreDtoValidator()
    {
        Include(new GenreCreationDtoValidator());
    }
}
