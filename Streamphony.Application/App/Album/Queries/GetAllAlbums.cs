using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.App.Albums.Queries;

public class GetAllAlbums() : IRequest<IEnumerable<AlbumDto>>;

public class GetAllAlbumsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllAlbums, IEnumerable<AlbumDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<IEnumerable<AlbumDto>> Handle(GetAllAlbums request, CancellationToken cancellationToken)
    {
        var albums = await _unitOfWork.AlbumRepository.GetAll(cancellationToken);

        _logger.LogSuccess(nameof(Album));
        return _mapper.Map<IEnumerable<AlbumDto>>(albums);
    }
}