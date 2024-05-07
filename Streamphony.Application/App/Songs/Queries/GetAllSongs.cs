using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.App.Songs.Queries;

public class GetAllSongs() : IRequest<IEnumerable<SongDto>>;

public class GetAllSongsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllSongs, IEnumerable<SongDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<IEnumerable<SongDto>> Handle(GetAllSongs request, CancellationToken cancellationToken)
    {
        var songs = await _unitOfWork.SongRepository.GetAll(cancellationToken);

        _logger.LogSuccess(nameof(Song));
        return _mapper.Map<IEnumerable<SongDto>>(songs);
    }
}