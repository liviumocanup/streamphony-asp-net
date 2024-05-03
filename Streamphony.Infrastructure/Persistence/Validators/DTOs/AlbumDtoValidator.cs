using FluentValidation;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Infrastructure.Persistence.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Persistence.Validators.DTOs;

public class AlbumDtoValidator : AbstractValidator<AlbumDto>
{
    public AlbumDtoValidator()
    {
        Include(new AlbumCreationDtoValidator());
    }
}
