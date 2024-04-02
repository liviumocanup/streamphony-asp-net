using AutoMapper;
using MediatR;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Users.Queries;

public record GetUserById(Guid Id) : IRequest<UserDto>;

public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById<User>(request.Id);

        return _mapper.Map<UserDto>(user);
    }
}