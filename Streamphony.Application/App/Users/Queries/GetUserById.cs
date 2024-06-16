using MediatR;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Users.Queries;

public record GetUserById(Guid Id) : IRequest<UserDto>;

public class GetUserByIdHandler(
    IUserManagerProvider userManagerProvider,
    IMappingProvider mapper,
    ILoggingService logger) : IRequestHandler<GetUserById, UserDto>
{
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _userManagerProvider.FindByIdAsync(request.Id.ToString());
        if (user == null)
            _logger.LogAndThrowNotFoundException(nameof(User), request.Id, LogAction.Get);

        _logger.LogSuccess(nameof(User), user!.Id, LogAction.Get);
        return _mapper.Map<UserDto>(user);
    }
}
