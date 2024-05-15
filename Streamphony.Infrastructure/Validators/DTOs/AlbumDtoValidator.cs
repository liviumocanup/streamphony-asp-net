using FluentValidation;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Infrastructure.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validators.DTOs;

public class AlbumDtoValidator : AbstractValidator<AlbumDto>
{
    public AlbumDtoValidator()
    {
        Include(new AlbumCreationDtoValidator());
    }
}
