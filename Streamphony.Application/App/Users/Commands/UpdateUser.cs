using AutoMapper;
using MediatR;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Commands;

public record UpdateUser(UserDto UserDto) : IRequest<UserDto>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, UserDto>
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public UpdateUserHandler(IRepository<User> userRepository, IMapper mapper, ILoggingService loggingService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<UserDto> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var id = request.UserDto.Id;
        var user = await _userRepository.GetById(id) ?? throw new KeyNotFoundException($"User with ID {id} not found.");

        _mapper.Map(request.UserDto, user);
        await _userRepository.SaveChangesAsync();
        await _loggingService.LogAsync($"User id {user.Id} - updated");

        return _mapper.Map<UserDto>(user);
    }
}
