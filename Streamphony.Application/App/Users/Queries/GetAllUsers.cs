using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Users.Responses;

namespace Streamphony.Application.App.Users.Queries;

public class GetAllUsers() : IRequest<IEnumerable<UserDto>>;

public class GetAllUsersHandler(IUnitOfWork unitOfWork, IMappingProvider mapper) : IRequestHandler<GetAllUsers, IEnumerable<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}