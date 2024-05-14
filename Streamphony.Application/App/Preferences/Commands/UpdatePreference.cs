using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Preferences.Responses;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Preferences.Commands;

public record UpdatePreference(PreferenceDto PreferenceDto) : IRequest<PreferenceDto>;

public class UpdatePreferenceHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger, IValidationService validationService) : IRequestHandler<UpdatePreference, PreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<PreferenceDto> Handle(UpdatePreference request, CancellationToken cancellationToken)
    {
        var preferenceDto = request.PreferenceDto;
        var preference = await _validationService.GetExistingEntity(_unitOfWork.PreferenceRepository, preferenceDto.Id, cancellationToken);

        _mapper.Map(preferenceDto, preference);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Preference), preference.Id, LogAction.Update);
        return _mapper.Map<PreferenceDto>(preference);
    }
}
