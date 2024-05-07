using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.Application.App.Albums.Queries;

public class GetAllAlbums() : IRequest<IEnumerable<AlbumDto>>;

public class GetAllAlbumsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger) : IRequestHandler<GetAllAlbums, IEnumerable<AlbumDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;

    public async Task<IEnumerable<AlbumDto>> Handle(GetAllAlbums request, CancellationToken cancellationToken)
    {
        var albums = await _unitOfWork.AlbumRepository.GetAll(cancellationToken);

        _logger.LogInformation("Retrieved all {EntityType}s.", nameof(Album));
        return _mapper.Map<IEnumerable<AlbumDto>>(albums);
    }
}