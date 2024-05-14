using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Albums.Queries;

public record GetAllAlbums(PagedRequest PagedRequest) : IRequest<PaginatedResult<AlbumDto>>;

public class GetAllAlbumsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllAlbums, PaginatedResult<AlbumDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<AlbumDto>> Handle(GetAllAlbums request, CancellationToken cancellationToken)
    {
        (var paginatedResult, var albumList) = await _unitOfWork.AlbumRepository.GetAllPaginated<AlbumDto>(request.PagedRequest, cancellationToken);

        paginatedResult.Items = _mapper.Map<IEnumerable<AlbumDto>>(albumList);

        _logger.LogSuccess(nameof(Album));
        return paginatedResult;
    }
}