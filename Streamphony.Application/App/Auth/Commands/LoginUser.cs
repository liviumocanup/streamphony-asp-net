using MediatR;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Auth.Commands;

public record LoginUser(LoginUserDto LoginUserDto) : IRequest<AuthResultDto>;

public class LoginUserHandler(
    ILoggingService loggingService,
    IUserManagerProvider userManagerProvider,
    IAuthenticationService authenticationService)
    : IRequestHandler<LoginUser, AuthResultDto>
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly ILoggingService _loggingService = loggingService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<AuthResultDto> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        var userDto = request.LoginUserDto;

        var userDb = await _userManagerProvider.FindByNameAsync(userDto.UserName);
        if (userDb == null)
            _loggingService.LogAndThrowNotFoundException(nameof(User), nameof(userDto.UserName), userDto.UserName);

        var token = await _authenticationService.Login(userDb!.Id, userDto.Password);
        if (token == null) _loggingService.LogAndThrowNotAuthorizedException(nameof(User));

        return new AuthResultDto(token!);
    }
}
