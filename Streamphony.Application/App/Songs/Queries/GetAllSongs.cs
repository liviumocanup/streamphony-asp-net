using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Models;

namespace Streamphony.Application.App.Songs.Queries;

public class GetAllSongs(int pageNumber, int pageSize) : IRequest<PaginatedResult<SongDto>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}

public class GetAllSongsHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllSongs, PaginatedResult<SongDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<SongDto>> Handle(GetAllSongs request, CancellationToken cancellationToken)
    {
        (IEnumerable<Song> songs, int totalRecords) = await _unitOfWork.SongRepository.GetAllPaginated(request.PageNumber, request.PageSize, cancellationToken);

        _logger.LogSuccess(nameof(Song));
        return new PaginatedResult<SongDto>(_mapper.Map<IEnumerable<SongDto>>(songs), totalRecords);
    }
}