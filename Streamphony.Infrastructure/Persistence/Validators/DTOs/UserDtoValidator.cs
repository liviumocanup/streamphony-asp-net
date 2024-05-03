using FluentValidation;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Infrastructure.Persistence.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Persistence.Validators.DTOs;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        Include(new UserCreationDtoValidator());
    }
}
