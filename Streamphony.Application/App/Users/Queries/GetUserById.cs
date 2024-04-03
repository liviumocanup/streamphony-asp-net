using AutoMapper;
using MediatR;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Queries;

public record GetUserById(Guid Id) : IRequest<UserDto>;

public class GetUserByIdHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserById, UserDto>
{
    private readonly IRepository<User> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(request.Id);

        return _mapper.Map<UserDto>(user);
    }
}