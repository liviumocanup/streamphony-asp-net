using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Albums.Queries;

public class GetAllAlbums(int pageNumber, int pageSize) : IRequest<PaginatedResult<AlbumDto>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}

public class GetAllAlbumsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllAlbums, PaginatedResult<AlbumDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<AlbumDto>> Handle(GetAllAlbums request, CancellationToken cancellationToken)
    {
        (IEnumerable<Album> albums, int totalRecords) = await _unitOfWork.AlbumRepository.GetAllPaginated(request.PageNumber, request.PageSize, cancellationToken);

        _logger.LogSuccess(nameof(Album));
        return new PaginatedResult<AlbumDto>(_mapper.Map<IEnumerable<AlbumDto>>(albums), totalRecords);
    }
}