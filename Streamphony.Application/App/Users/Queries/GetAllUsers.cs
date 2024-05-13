using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Users.Queries;

public class GetAllUsers(int pageNumber, int pageSize) : IRequest<PaginatedResult<UserDto>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}

public class GetAllUsersHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllUsers, PaginatedResult<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        (IEnumerable<User> users, int totalRecords) = await _unitOfWork.UserRepository.GetAllPaginated(request.PageNumber, request.PageSize, cancellationToken);

        _logger.LogSuccess(nameof(User));
        return new PaginatedResult<UserDto>(_mapper.Map<IEnumerable<UserDto>>(users), totalRecords);
    }
}