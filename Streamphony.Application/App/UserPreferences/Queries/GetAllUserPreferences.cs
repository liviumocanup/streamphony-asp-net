using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Models;

namespace Streamphony.Application.App.UserPreferences.Queries;

public class GetAllUserPreferences(int pageNumber, int pageSize) : IRequest<PaginatedResult<UserPreferenceDto>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}

public class GetAllUserPreferencesHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllUserPreferences, PaginatedResult<UserPreferenceDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<UserPreferenceDto>> Handle(GetAllUserPreferences request, CancellationToken cancellationToken)
    {
        (IEnumerable<UserPreference> userPreferences, int totalRecords) = await _unitOfWork.UserPreferenceRepository.GetAllPaginated(request.PageNumber, request.PageSize, cancellationToken);

        _logger.LogSuccess(nameof(Album));
        return new PaginatedResult<UserPreferenceDto>(_mapper.Map<IEnumerable<UserPreferenceDto>>(userPreferences), totalRecords);
    }
}