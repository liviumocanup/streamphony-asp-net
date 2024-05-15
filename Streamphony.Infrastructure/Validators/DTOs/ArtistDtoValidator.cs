using FluentValidation;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Infrastructure.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validators.DTOs;

public class ArtistDtoValidator : AbstractValidator<ArtistDto>
{
    public ArtistDtoValidator()
    {
        Include(new ArtistCreationDtoValidator());
    }
}
