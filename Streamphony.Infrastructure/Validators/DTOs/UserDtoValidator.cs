using FluentValidation;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Infrastructure.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validators.DTOs;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        Include(new UserCreationDtoValidator());
    }
}
