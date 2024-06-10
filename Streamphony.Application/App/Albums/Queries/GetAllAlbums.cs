using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Queries;

public record GetAllAlbums(PagedRequest PagedRequest) : IRequest<PaginatedResult<AlbumDto>>;

public class GetAllAlbumsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger)
    : IRequestHandler<GetAllAlbums, PaginatedResult<AlbumDto>>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<AlbumDto>> Handle(GetAllAlbums request, CancellationToken cancellationToken)
    {
        var (paginatedResult, albumList) =
            await _unitOfWork.AlbumRepository.GetAllPaginated<AlbumDto>(request.PagedRequest, cancellationToken);

        paginatedResult.Items = _mapper.Map<IEnumerable<AlbumDto>>(albumList);

        _logger.LogSuccess(nameof(Album));
        return paginatedResult;
    }
}
