using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
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
    private readonly ILoggingService _logger;
    private readonly IValidationService _validationService;
    public CreateUserHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger, IValidationService validationService)
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

        _logger.LogSuccess(nameof(User), userDb.Id);
        return _mapper.Map<UserDto>(userDb);
    }

    private async Task EnsureUniqueUsernameAndEmail(string username, string email, CancellationToken cancellationToken)
    {
        var conflictingUser = await _unitOfWork.UserRepository.GetByUsernameOrEmail(username, email, cancellationToken);
        if (conflictingUser != null)
        {
            if (conflictingUser.Username == username)
            {
                _logger.LogAndThrowDuplicateException(nameof(User), "Username", username, LogAction.Create);
            }
            if (conflictingUser.Email == email)
            {
                _logger.LogAndThrowDuplicateException(nameof(User), "Email", email, LogAction.Create);
            }
        }
    }
}