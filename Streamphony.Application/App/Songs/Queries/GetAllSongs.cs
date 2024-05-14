using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Songs.Queries;

public record GetAllSongs(PagedRequest PagedRequest) : IRequest<PaginatedResult<SongDto>>;

public class GetAllSongsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllSongs, PaginatedResult<SongDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<SongDto>> Handle(GetAllSongs request, CancellationToken cancellationToken)
    {
        (var paginatedResult, var songList) = await _unitOfWork.SongRepository.GetAllPaginated<SongDto>(request.PagedRequest, cancellationToken);

        paginatedResult.Items = _mapper.Map<IEnumerable<SongDto>>(songList);

        _logger.LogSuccess(nameof(Song));
        return paginatedResult;
    }
}