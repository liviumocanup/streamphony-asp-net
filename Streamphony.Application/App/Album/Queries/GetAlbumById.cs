using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.Application.App.Albums.Queries;

public record GetAlbumById(Guid Id) : IRequest<AlbumDetailsDto>;

public class GetAlbumByIdHandler(IUnitOfWork unitOfWork, IMappingProvider mapper) : IRequestHandler<GetAlbumById, AlbumDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;

    public async Task<AlbumDetailsDto> Handle(GetAlbumById request, CancellationToken cancellationToken)
    {
        var album = await _unitOfWork.AlbumRepository.GetByIdWithInclude(request.Id, cancellationToken, album => album.Songs);

        return _mapper.Map<AlbumDetailsDto>(album);
    }
}