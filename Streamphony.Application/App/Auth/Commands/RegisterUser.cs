using MediatR;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.Application.Common;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Auth.Commands;

public record RegisterUser(RegisterUserDto RegisterUserDto) : IRequest<AuthResultDto>;

public class RegisterUserHandler : IRequestHandler<RegisterUser, AuthResultDto>
{
    private readonly IMappingProvider _mapper;
    private readonly ILoggingService _loggingService;
    private readonly IUserManagerProvider _userManagerProvider;
    private readonly IAuthenticationService _authenticationService;

    public RegisterUserHandler(IMappingProvider mapper, ILoggingService loggingService, IUserManagerProvider userManagerProvider, IAuthenticationService authenticationService)
    {
        _mapper = mapper;
        _loggingService = loggingService;
        _userManagerProvider = userManagerProvider;
        _authenticationService = authenticationService;
    }

    public async Task<AuthResultDto> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        var userDto = request.RegisterUserDto;
        var userEntity = _mapper.Map<User>(userDto);

        var userDb = await _userManagerProvider.FindByNameAsync(userDto.UserName);
        if (userDb != null) _loggingService.LogAndThrowDuplicateException(nameof(User), nameof(userDto.UserName), userDto.UserName, LogAction.Create);

        var token = await _authenticationService.Register(userEntity, userDto.Password, userDto.FirstName, userDto.LastName, userDto.Role.ToString());

        return new AuthResultDto(token);
    }
}
