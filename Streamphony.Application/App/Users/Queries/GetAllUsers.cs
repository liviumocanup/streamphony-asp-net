using MediatR;
using AutoMapper;
using Streamphony.Application.Abstractions;
using Streamphony.Application.App.Users.Responses;

namespace Streamphony.Application.App.Users.Queries;

public class GetAllUsers() : IRequest<IEnumerable<UserDto>>;

public class GetAllUsersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUsers, IEnumerable<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAll();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}