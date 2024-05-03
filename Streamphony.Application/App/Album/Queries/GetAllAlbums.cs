using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.Application.App.Albums.Queries;

public class GetAllAlbums() : IRequest<IEnumerable<AlbumDto>>;

public class GetAllAlbumsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllAlbums, IEnumerable<AlbumDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<AlbumDto>> Handle(GetAllAlbums request, CancellationToken cancellationToken)
    {
        var albums = await _unitOfWork.AlbumRepository.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<AlbumDto>>(albums);
    }
}