using FluentValidation;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Infrastructure.Validation.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validation.Validators.DTOs;

public class AlbumDtoValidator : AbstractValidator<AlbumDto>
{
    public AlbumDtoValidator()
    {
        Include(new AlbumCreationDtoValidator());
    }
}
