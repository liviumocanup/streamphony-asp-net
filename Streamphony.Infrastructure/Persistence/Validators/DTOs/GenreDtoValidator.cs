using FluentValidation;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Infrastructure.Persistence.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Persistence.Validators.DTOs;

public class GenreDtoValidator : AbstractValidator<GenreDto>
{
    public GenreDtoValidator()
    {
        Include(new GenreCreationDtoValidator());
    }
}
