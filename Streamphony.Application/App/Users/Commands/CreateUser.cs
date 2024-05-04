using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Commands;

public record CreateUser(UserCreationDto UserCreationDto) : IRequest<UserDto>;

public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;
    public CreateUserHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
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

            _logger.LogInformation("Successfully created {EntityType} with Id {EntityId}.", nameof(User), userDb.Id);

            return _mapper.Map<UserDto>(userDb);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogError("Failed to create {EntityType}. Error: {Error}", nameof(User), ex.ToString());
            throw;
        }
    }
}