using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.DTOs;

namespace Streamphony.Application.App.Songs.Queries;

public record GetSongForArtist(Guid Id) : IRequest<IEnumerable<SongDto>>;

public class GetSongsForArtistHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider) : IRequestHandler<GetSongForArtist, IEnumerable<SongDto>>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<IEnumerable<SongDto>> Handle(GetSongForArtist request, CancellationToken cancellationToken)
    {
        var userDb = await _userManagerProvider.FindByIdAsync(request.Id.ToString());
        var artistId = userDb!.ArtistId!.Value;
        
        var songs = await _unitOfWork.SongRepository.GetByOwnerId(artistId, cancellationToken);

        return _mapper.Map<IEnumerable<SongDto>>(songs);
    }
}
