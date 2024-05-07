using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Users.Commands;

public record UpdateUser(UserDto UserDto) : IRequest<UserDto>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingService _logger;
    private readonly IValidationService _validationService;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger, IValidationService validationService)
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

        _logger.LogSuccess(nameof(User), user.Id, LogAction.Update);
        return _mapper.Map<UserDto>(user);
    }

    private async Task EnsureUniqueUsernameAndEmailExceptId(string username, string email, Guid id, CancellationToken cancellationToken)
    {
        var conflictingUser = await _unitOfWork.UserRepository.GetByUsernameOrEmailWhereIdNotEqual(username, email, id, cancellationToken);
        if (conflictingUser != null)
        {
            if (conflictingUser.Username == username)
            {
                _logger.LogAndThrowDuplicateException(nameof(User), "Username", username, LogAction.Update);
            }
            if (conflictingUser.Email == email)
            {
                _logger.LogAndThrowDuplicateException(nameof(User), "Email", email, LogAction.Update);
            }
        }
    }
}
