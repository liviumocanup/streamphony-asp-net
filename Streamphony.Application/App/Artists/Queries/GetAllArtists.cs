using MediatR;
using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Artists.Queries;

public record GetAllArtists(PagedRequest PagedRequest) : IRequest<PaginatedResult<ArtistDetailsDto>>;

public class GetAllArtistsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger)
    : IRequestHandler<GetAllArtists, PaginatedResult<ArtistDetailsDto>>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<ArtistDetailsDto>> Handle(GetAllArtists request,
        CancellationToken cancellationToken)
    {
        var (paginatedResult, artistList) =
            await _unitOfWork.ArtistRepository.GetAllPaginated<ArtistDetailsDto>(request.PagedRequest,
                cancellationToken, query => query.Include(a => a.ProfilePictureBlob));

        paginatedResult.Items = _mapper.Map<IEnumerable<ArtistDetailsDto>>(artistList);

        _logger.LogSuccess(nameof(Artist));
        return paginatedResult;
    }
}
