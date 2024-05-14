using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Preferences.Responses;
using Streamphony.Application.Common;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.App.Preferences.Queries;

public record GetPreferenceById(Guid Id) : IRequest<PreferenceDto>;

public class GetPreferenceByIdHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger, IValidationService validationService) : IRequestHandler<GetPreferenceById, PreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<PreferenceDto> Handle(GetPreferenceById request, CancellationToken cancellationToken)
    {
        var preference = await _validationService.GetExistingEntity(_unitOfWork.PreferenceRepository, request.Id, cancellationToken, LogAction.Get);

        _logger.LogSuccess(nameof(Preference), preference.Id, LogAction.Get);
        return _mapper.Map<PreferenceDto>(preference);
    }
}