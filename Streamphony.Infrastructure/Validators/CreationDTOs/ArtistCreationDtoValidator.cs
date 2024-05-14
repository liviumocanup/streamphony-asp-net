using FluentValidation;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Infrastructure.Extensions;

namespace Streamphony.Infrastructure.Validators.CreationDTOs;

public class ArtistCreationDtoValidator : AbstractValidator<ArtistCreationDto>
{
    public ArtistCreationDtoValidator()
    {
        RuleFor(artist => artist.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(artist => artist.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(artist => artist.DateOfBirth)
            .DateNotInFuture();

        RuleFor(artist => artist.ProfilePictureUrl)
            .MaximumLength(1000)
            .ValidUrl();
    }
}
