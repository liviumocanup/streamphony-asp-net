using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Users.Commands;

public record CreateUser(UserCreationDto UserCreationDto) : IRequest<UserDto>;

public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;
    private readonly IValidationService _validationService;
    public CreateUserHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var userDto = request.UserCreationDto;

        await EnsureUniqueUsernameAndEmail(userDto.Username, userDto.Email, cancellationToken);

        var userEntity = _mapper.Map<User>(userDto);
        var userDb = await _unitOfWork.UserRepository.Add(userEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Create, nameof(User), userDb.Id);
        return _mapper.Map<UserDto>(userDb);
    }

    private async Task EnsureUniqueUsernameAndEmail(string username, string email, CancellationToken cancellationToken)
    {
        var conflictingUsers = await _unitOfWork.UserRepository.GetByUsernameOrEmail(username, email, cancellationToken);
        var user = conflictingUsers.FirstOrDefault();
        if (user != null)
        {
            if (user.Username == username)
            {
                _validationService.LogAndThrowDuplicateException(nameof(User), "Username", username, LogAction.Create);
            }
            if (user.Email == email)
            {
                _validationService.LogAndThrowDuplicateException(nameof(User), "Email", email, LogAction.Create);
            }
        }
    }
}