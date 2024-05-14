using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Preferences.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Preferences.Queries;

public class GetAllPreferences(int pageNumber, int pageSize) : IRequest<PaginatedResult<PreferenceDto>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}

public class GetAllPreferencesHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllPreferences, PaginatedResult<PreferenceDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<PreferenceDto>> Handle(GetAllPreferences request, CancellationToken cancellationToken)
    {
        (IEnumerable<Preference> preferences, int totalRecords) = await _unitOfWork.PreferenceRepository.GetAllPaginated(request.PageNumber, request.PageSize, cancellationToken);

        _logger.LogSuccess(nameof(Album));
        return new PaginatedResult<PreferenceDto>(_mapper.Map<IEnumerable<PreferenceDto>>(preferences), totalRecords);
    }
}