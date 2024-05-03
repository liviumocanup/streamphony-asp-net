using FluentValidation;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.Infrastructure.Persistence.Validators.CreationDTOs;

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
