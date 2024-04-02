using Streamphony.Application.App.Users.Responses;
using MediatR;
using Streamphony.Application.Interfaces.Repositories;
using AutoMapper;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Queries;

public class GetAllUsers() : IRequest<IEnumerable<UserDto>>;

public class GetAllUsersHandler : IRequestHandler<GetAllUsers, IEnumerable<UserDto>>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public GetAllUsersHandler(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAll<User>();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}