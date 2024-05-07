using FluentValidation;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Infrastructure.Extensions;

namespace Streamphony.Infrastructure.Validation.Validators.CreationDTOs;

public class AlbumCreationDtoValidator : AbstractValidator<AlbumCreationDto>
{
    public AlbumCreationDtoValidator()
    {
        RuleFor(album => album.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(album => album.CoverImageUrl)
            .MaximumLength(1000)
            .ValidUrl();
    }
}
