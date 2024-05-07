using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.UserPreferences.Commands;

public record UpdateUserPreference(UserPreferenceDto UserPreferenceDto) : IRequest<UserPreferenceDto>;

public class UpdateUserPreferenceHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<UpdateUserPreference, UserPreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<UserPreferenceDto> Handle(UpdateUserPreference request, CancellationToken cancellationToken)
    {
        var userPreferenceDto = request.UserPreferenceDto;
        var userPreference = await _validationService.GetExistingEntity(_unitOfWork.UserPreferenceRepository, userPreferenceDto.Id, cancellationToken);

        _mapper.Map(userPreferenceDto, userPreference);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Update, nameof(UserPreference), userPreference.Id);
        return _mapper.Map<UserPreferenceDto>(userPreference);
    }
}
