using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Auth.Responses;

public record RegisterUserDto(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password,
    RoleEnum Role
);
