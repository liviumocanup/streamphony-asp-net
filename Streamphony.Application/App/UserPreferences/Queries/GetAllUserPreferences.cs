using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.App.UserPreferences.Queries;

public class GetAllUserPreferences() : IRequest<IEnumerable<UserPreferenceDto>>;

public class GetAllUserPreferencesHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllUserPreferences, IEnumerable<UserPreferenceDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<IEnumerable<UserPreferenceDto>> Handle(GetAllUserPreferences request, CancellationToken cancellationToken)
    {
        var userPreferences = await _unitOfWork.UserPreferenceRepository.GetAll(cancellationToken);

        _logger.LogSuccess(nameof(UserPreference));
        return _mapper.Map<IEnumerable<UserPreferenceDto>>(userPreferences);
    }
}