using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.UserPreferences.Commands;

public record CreateUserPreference(UserPreferenceDto UserPreferenceDto) : IRequest<UserPreferenceDto>;

public class CreateUserPreferenceHandler : IRequestHandler<CreateUserPreference, UserPreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;
    private readonly IValidationService _validationService;

    public CreateUserPreferenceHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<UserPreferenceDto> Handle(CreateUserPreference request, CancellationToken cancellationToken)
    {
        var userPreferenceDto = request.UserPreferenceDto;
        var getByIdFunc = _unitOfWork.UserPreferenceRepository.GetById;

        await _validationService.AssertNavigationEntityExists<UserPreference, User>(_unitOfWork.UserRepository, userPreferenceDto.Id, cancellationToken);
        await _validationService.EnsureUniqueProperty(getByIdFunc, nameof(userPreferenceDto.Id), userPreferenceDto.Id, cancellationToken);

        var userPreferenceEntity = _mapper.Map<UserPreference>(userPreferenceDto);
        var userPreferenceDb = await _unitOfWork.UserPreferenceRepository.Add(userPreferenceEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Create, nameof(UserPreference), userPreferenceDb.Id);
        return _mapper.Map<UserPreferenceDto>(userPreferenceDb);
    }
}