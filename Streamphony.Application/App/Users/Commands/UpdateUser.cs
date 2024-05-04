using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Commands;

public record UpdateUser(UserDto UserDto) : IRequest<UserDto>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UserDto> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var userDto = request.UserDto;
        var user = await _unitOfWork.UserRepository.GetById(userDto.Id, cancellationToken) ??
                    throw new KeyNotFoundException($"User with ID {userDto.Id} not found.");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            _mapper.Map(userDto, user);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            _logger.LogInformation("Successfully updated {EntityType} with Id {EntityId}.", nameof(User), user.Id);

            return _mapper.Map<UserDto>(user);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogError("Failed to update {EntityType}. Error: {Error}", nameof(User), ex.ToString());
            throw;
        }
    }
}
