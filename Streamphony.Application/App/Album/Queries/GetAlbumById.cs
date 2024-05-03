using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.Application.App.Albums.Queries;

public record GetAlbumById(Guid Id) : IRequest<AlbumDetailsDto>;

public class GetAlbumByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAlbumById, AlbumDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<AlbumDetailsDto> Handle(GetAlbumById request, CancellationToken cancellationToken)
    {
        var album = await _unitOfWork.AlbumRepository.GetByIdWithInclude(request.Id, cancellationToken, album => album.Songs);

        return _mapper.Map<AlbumDetailsDto>(album);
    }
}