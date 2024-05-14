using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Artists.Queries;

public class GetAllArtists(int pageNumber, int pageSize) : IRequest<PaginatedResult<ArtistDto>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}

public class GetAllArtistsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllArtists, PaginatedResult<ArtistDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<ArtistDto>> Handle(GetAllArtists request, CancellationToken cancellationToken)
    {
        (IEnumerable<Artist> artists, int totalRecords) = await _unitOfWork.ArtistRepository.GetAllPaginated(request.PageNumber, request.PageSize, cancellationToken);

        _logger.LogSuccess(nameof(Artist));
        return new PaginatedResult<ArtistDto>(_mapper.Map<IEnumerable<ArtistDto>>(artists), totalRecords);
    }
}