using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Preferences.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Preferences.Queries;

public record GetAllPreferences(PagedRequest PagedRequest) : IRequest<PaginatedResult<PreferenceDto>>;

public class GetAllPreferencesHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllPreferences, PaginatedResult<PreferenceDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<PreferenceDto>> Handle(GetAllPreferences request, CancellationToken cancellationToken)
    {
        (var paginatedResult, var preferenceList) = await _unitOfWork.PreferenceRepository.GetAllPaginated<PreferenceDto>(request.PagedRequest, cancellationToken);

        paginatedResult.Items = _mapper.Map<IEnumerable<PreferenceDto>>(preferenceList);

        _logger.LogSuccess(nameof(Preference));
        return paginatedResult;
    }
}