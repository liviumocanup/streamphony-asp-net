using FluentValidation;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Infrastructure.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validators.DTOs;

public class SongDtoValidator : AbstractValidator<SongDto>
{
    public SongDtoValidator()
    {
        Include(new SongCreationDtoValidator());
    }
}
