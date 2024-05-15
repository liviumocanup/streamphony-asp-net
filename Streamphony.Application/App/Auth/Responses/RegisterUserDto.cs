using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Auth.Responses;

public record RegisterUserDto(string UserName, string Password, string Email, string FirstName, string LastName, RoleEnum Role);
