using FluentValidation;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Infrastructure.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validators.DTOs;

public class ArtistDtoValidator : AbstractValidator<ArtistDto>
{
    public ArtistDtoValidator()
    {
        
    }
}
