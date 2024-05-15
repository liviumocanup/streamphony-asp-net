using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Artists.Queries;

public record GetAllArtists(PagedRequest PagedRequest) : IRequest<PaginatedResult<ArtistDto>>;

public class GetAllArtistsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllArtists, PaginatedResult<ArtistDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<ArtistDto>> Handle(GetAllArtists request, CancellationToken cancellationToken)
    {
        (var paginatedResult, var artistList) = await _unitOfWork.ArtistRepository.GetAllPaginated<ArtistDto>(request.PagedRequest, cancellationToken);

        paginatedResult.Items = _mapper.Map<IEnumerable<ArtistDto>>(artistList);

        _logger.LogSuccess(nameof(Artist));
        return paginatedResult;
    }
}