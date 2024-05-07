using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Users.Responses;

namespace Streamphony.Application.App.Users.Queries;

public class GetAllUsers() : IRequest<IEnumerable<UserDto>>;

public class GetAllUsersHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger) : IRequestHandler<GetAllUsers, IEnumerable<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAll(cancellationToken);

        _logger.LogInformation("Retrieved all {EntityType}s.", nameof(User));
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}