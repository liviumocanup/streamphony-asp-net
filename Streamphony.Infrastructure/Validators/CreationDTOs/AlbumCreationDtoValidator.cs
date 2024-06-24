using FluentValidation;
using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Infrastructure.Extensions;

namespace Streamphony.Infrastructure.Validators.CreationDTOs;

public class AlbumCreationDtoValidator : AbstractValidator<AlbumCreationDto>
{
    public AlbumCreationDtoValidator()
    {
        RuleFor(album => album.Title)
            .NotEmpty()
            .MaximumLength(50);
    }
}
