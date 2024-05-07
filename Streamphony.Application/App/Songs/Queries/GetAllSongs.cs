using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.App.Songs.Queries;

public class GetAllSongs() : IRequest<IEnumerable<SongDto>>;

public class GetAllSongsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger) : IRequestHandler<GetAllSongs, IEnumerable<SongDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;

    public async Task<IEnumerable<SongDto>> Handle(GetAllSongs request, CancellationToken cancellationToken)
    {
        var songs = await _unitOfWork.SongRepository.GetAll(cancellationToken);

        _logger.LogInformation("Retrieved all {EntityType}s.", nameof(Song));
        return _mapper.Map<IEnumerable<SongDto>>(songs);
    }
}