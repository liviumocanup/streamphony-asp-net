using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Services;
using Streamphony.Application.Exceptions;

namespace Streamphony.Application.App.Users.Commands;

public record UpdateUser(UserDto UserDto) : IRequest<UserDto>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;
    private readonly IValidationService _validationService;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<UserDto> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var userDto = request.UserDto;
        var user = await _validationService.GetExistingEntity(_unitOfWork.UserRepository, userDto.Id, cancellationToken);
        await EnsureUniqueUsernameAndEmailExceptId(userDto.Username, userDto.Email, userDto.Id, cancellationToken);

        _mapper.Map(userDto, user);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Update, nameof(User), user.Id);
        return _mapper.Map<UserDto>(user);
    }

    private async Task EnsureUniqueUsernameAndEmailExceptId(string username, string email, Guid id, CancellationToken cancellationToken)
    {
        var conflictingUsers = await _unitOfWork.UserRepository.GetByUsernameOrEmailWhereIdNotEqual(username, email, id, cancellationToken);
        var user = conflictingUsers.FirstOrDefault();
        if (user != null)
        {
            if (user.Username == username)
            {
                _validationService.LogAndThrowDuplicateException(nameof(User), "Username", username, LogAction.Update);
            }
            if (user.Email == email)
            {
                _validationService.LogAndThrowDuplicateException(nameof(User), "Email", email, LogAction.Update);
            }
        }
    }
}
