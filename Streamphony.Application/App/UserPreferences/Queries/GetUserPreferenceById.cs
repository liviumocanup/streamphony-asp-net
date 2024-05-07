using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.Services;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.App.UserPreferences.Queries;

public record GetUserPreferenceById(Guid Id) : IRequest<UserPreferenceDto>;

public class GetUserPreferenceByIdHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<GetUserPreferenceById, UserPreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<UserPreferenceDto> Handle(GetUserPreferenceById request, CancellationToken cancellationToken)
    {
        var userPreference = await _validationService.GetExistingEntity(_unitOfWork.UserPreferenceRepository, request.Id, cancellationToken, LogAction.Get);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Get, nameof(UserPreference), userPreference.Id);
        return _mapper.Map<UserPreferenceDto>(userPreference);
    }
}