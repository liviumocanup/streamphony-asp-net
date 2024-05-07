using FluentValidation;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Infrastructure.Validation.Validators.CreationDTOs;

namespace Streamphony.Infrastructure.Validation.Validators.DTOs;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        Include(new UserCreationDtoValidator());
    }
}
