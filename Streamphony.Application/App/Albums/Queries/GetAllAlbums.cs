using MediatR;
using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Queries;

public record GetAllAlbums(PagedRequest PagedRequest) : IRequest<PaginatedResult<AlbumDetailsDto>>;

public class GetAllAlbumsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger)
    : IRequestHandler<GetAllAlbums, PaginatedResult<AlbumDetailsDto>>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<AlbumDetailsDto>> Handle(GetAllAlbums request,
        CancellationToken cancellationToken)
    {
        var (paginatedResult, albumList) =
            await _unitOfWork.AlbumRepository.GetAllPaginated<AlbumDetailsDto>(request.PagedRequest, cancellationToken,
                query => query.Include(a => a.CoverBlob)
                    .Include(a => a.Songs)
                    .Include(a => a.Owner)
                    .Include(a => a.Owner)
            );

        paginatedResult.Items = _mapper.Map<IEnumerable<AlbumDetailsDto>>(albumList);

        _logger.LogSuccess(nameof(Album));
        return paginatedResult;
    }
}
