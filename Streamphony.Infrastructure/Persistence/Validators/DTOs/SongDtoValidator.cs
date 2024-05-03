using FluentValidation;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Infrastructure.Persistence.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Persistence.Validators.DTOs;

public class SongDtoValidator : AbstractValidator<SongDto>
{
    public SongDtoValidator()
    {
        Include(new SongCreationDtoValidator());
    }
}
