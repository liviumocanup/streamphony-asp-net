using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Queries;

public record GetAllSongs(PagedRequest PagedRequest) : IRequest<PaginatedResult<SongDto>>;

public class GetAllSongsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger)
    : IRequestHandler<GetAllSongs, PaginatedResult<SongDto>>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<SongDto>> Handle(GetAllSongs request, CancellationToken cancellationToken)
    {
        var (paginatedResult, songList) =
            await _unitOfWork.SongRepository.GetAllPaginated<SongDto>(request.PagedRequest, cancellationToken);

        paginatedResult.Items = _mapper.Map<IEnumerable<SongDto>>(songList);

        _logger.LogSuccess(nameof(Song));
        return paginatedResult;
    }
}
