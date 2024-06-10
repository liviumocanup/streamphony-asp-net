using MediatR;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.Application.Common;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Auth.Commands;

public record RegisterUser(RegisterUserDto RegisterUserDto) : IRequest<AuthResultDto>;

public class RegisterUserHandler(
    IMappingProvider mapper,
    ILoggingService loggingService,
    IUserManagerProvider userManagerProvider,
    IAuthenticationService authenticationService)
    : IRequestHandler<RegisterUser, AuthResultDto>
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly ILoggingService _loggingService = loggingService;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<AuthResultDto> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        var userDto = request.RegisterUserDto;
        var userEntity = _mapper.Map<User>(userDto);

        var userDb = await _userManagerProvider.FindByNameAsync(userDto.Username);
        if (userDb != null)
            _loggingService.LogAndThrowDuplicateException(nameof(User), nameof(userDto.Username), userDto.Username,
                LogAction.Create);

        var token = await _authenticationService.Register(userEntity, userDto.Password, userDto.FirstName,
            userDto.LastName, userDto.Role.ToString());

        return new AuthResultDto(token);
    }
}
