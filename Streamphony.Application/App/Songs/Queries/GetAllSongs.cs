using MediatR;
using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Queries;

public record GetAllSongs(PagedRequest PagedRequest) : IRequest<PaginatedResult<SongDetailsDto>>;

public class GetAllSongsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger)
    : IRequestHandler<GetAllSongs, PaginatedResult<SongDetailsDto>>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<SongDetailsDto>> Handle(GetAllSongs request, CancellationToken cancellationToken)
    {
        var (paginatedResult, songList) =
            await _unitOfWork.SongRepository.GetAllPaginated<SongDetailsDto>(request.PagedRequest, cancellationToken,
                query => query.Include(s => s.CoverBlob)
                    .Include(s => s.AudioBlob)
                    .Include(s => s.Album)
                    .Include(s => s.Owner));

        paginatedResult.Items = _mapper.Map<IEnumerable<SongDetailsDto>>(songList);

        _logger.LogSuccess(nameof(Song));
        return paginatedResult;
    }
}
