using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.App.Songs.Queries;

public class GetAllSongs() : IRequest<IEnumerable<SongDto>>;

public class GetAllSongsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper) : IRequestHandler<GetAllSongs, IEnumerable<SongDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;

    public async Task<IEnumerable<SongDto>> Handle(GetAllSongs request, CancellationToken cancellationToken)
    {
        var songs = await _unitOfWork.SongRepository.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<SongDto>>(songs);
    }
}