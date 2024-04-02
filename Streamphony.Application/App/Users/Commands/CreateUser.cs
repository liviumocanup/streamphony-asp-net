using AutoMapper;
using MediatR;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Commands;

public record CreateUser(UserDto UserDto) : IRequest<UserDto>;

public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateUserHandler(IRepository repository, IMapper mapper, ILoggingService loggingService)
    {
        _repository = repository;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = _mapper.Map<User>(request.UserDto);

            _repository.Add(userEntity);
            await _repository.SaveChangesAsync();

            await _loggingService.LogAsync($"User id {userEntity.Id} - success", nameof(CreateUser));

            return _mapper.Map<UserDto>(userEntity);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            await _loggingService.LogAsync($"Creation failure: {errorMessage}", nameof(CreateUser));
            throw;
        }
    }
}