using FluentValidation;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Infrastructure.Extensions;

namespace Streamphony.Infrastructure.Validators.CreationDTOs;

public class ArtistCreationDtoValidator : AbstractValidator<ArtistCreationDto>
{
    public ArtistCreationDtoValidator()
    {
        RuleFor(artist => artist.StageName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(artist => artist.DateOfBirth)
            .DateNotInFuture();
    }
}
