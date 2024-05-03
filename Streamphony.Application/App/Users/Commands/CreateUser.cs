using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Commands;

public record CreateUser(UserCreationDto UserCreationDto) : IRequest<UserDto>;

public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService loggingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var userEntity = _mapper.Map<User>(request.UserCreationDto);

        if (await _unitOfWork.UserRepository.GetByUsername(userEntity.Username, cancellationToken) != null)
            throw new Exception($"User with username {userEntity.Username} already exists");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var userDb = await _unitOfWork.UserRepository.Add(userEntity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"User id {userDb.Id} - success");

            return _mapper.Map<UserDto>(userDb);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Creation failure: ", ex);
            throw;
        }
    }
}