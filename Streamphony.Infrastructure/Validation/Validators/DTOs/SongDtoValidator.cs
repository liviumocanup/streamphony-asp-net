using FluentValidation;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Infrastructure.Validation.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validation.Validators.DTOs;

public class SongDtoValidator : AbstractValidator<SongDto>
{
    public SongDtoValidator()
    {
        Include(new SongCreationDtoValidator());
    }
}
