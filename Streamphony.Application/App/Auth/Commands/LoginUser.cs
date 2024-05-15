using MediatR;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Auth.Commands;

public record LoginUser(LoginUserDto LoginUserDto) : IRequest<AuthResultDto>;

public class LoginUserHandler : IRequestHandler<LoginUser, AuthResultDto>
{
    private readonly ILoggingService _loggingService;
    private readonly IUserManagerProvider _userManagerProvider;
    private readonly IAuthenticationService _authenticationService;

    public LoginUserHandler(ILoggingService loggingService, IUserManagerProvider userManagerProvider, IAuthenticationService authenticationService)
    {
        _loggingService = loggingService;
        _userManagerProvider = userManagerProvider;
        _authenticationService = authenticationService;
    }

    public async Task<AuthResultDto> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        var userDto = request.LoginUserDto;

        var userDb = await _userManagerProvider.FindByNameAsync(userDto.UserName);
        if (userDb == null) _loggingService.LogAndThrowNotFoundException(nameof(User), nameof(userDto.UserName), userDto.UserName);

        var token = await _authenticationService.Login(userDb!, userDto.Password);
        if (token == null) _loggingService.LogAndThrowNotAuthorizedException(nameof(User));

        return new AuthResultDto(token!);
    }
}