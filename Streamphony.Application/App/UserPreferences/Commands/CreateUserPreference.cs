using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.UserPreferences.Commands;

public record CreateUserPreference(UserPreferenceDto UserPreferenceDto) : IRequest<UserPreferenceDto>;

public class CreateUserPreferenceHandler : IRequestHandler<CreateUserPreference, UserPreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateUserPreferenceHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService loggingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<UserPreferenceDto> Handle(CreateUserPreference request, CancellationToken cancellationToken)
    {
        var userPreferenceDto = request.UserPreferenceDto;
        if (await _unitOfWork.UserRepository.GetById(userPreferenceDto.Id, cancellationToken) == null)
            throw new KeyNotFoundException($"User with ID {userPreferenceDto.Id} not found.");

        var userPreferenceEntity = _mapper.Map<UserPreference>(userPreferenceDto);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var userPreferenceDb = await _unitOfWork.UserPreferenceRepository.Add(userPreferenceEntity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"UserPreference for User {userPreferenceDb.Id} - success");

            return _mapper.Map<UserPreferenceDto>(userPreferenceDb);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Creation failure: ", ex);
            throw;
        }
    }
}