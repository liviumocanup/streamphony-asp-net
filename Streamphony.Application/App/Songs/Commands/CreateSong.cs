using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record CreateSong(SongCreationDto SongCreationDto) : IRequest<SongDto>;

public class CreateSongHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService)
    : IRequestHandler<CreateSong, SongDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<SongDto> Handle(CreateSong request, CancellationToken cancellationToken)
    {
        var songDto = request.SongCreationDto;
        var getByOwnerIdAndTitleFunc = _unitOfWork.SongRepository.GetByOwnerIdAndTitle;

        await _validationService.AssertNavigationEntityExists<Song, Artist>(_unitOfWork.ArtistRepository,
            songDto.OwnerId, cancellationToken);
        await _validationService.AssertNavigationEntityExists<Song, Genre>(_unitOfWork.GenreRepository, songDto.GenreId,
            cancellationToken);
        await _validationService.AssertNavigationEntityExists<Song, Album>(_unitOfWork.AlbumRepository, songDto.AlbumId,
            cancellationToken);
        await _validationService.EnsureArtistUniqueProperty(getByOwnerIdAndTitleFunc, songDto.OwnerId,
            nameof(songDto.Title), songDto.Title, cancellationToken);

        var songEntity = _mapper.Map<Song>(songDto);
        var songDb = await _unitOfWork.SongRepository.Add(songEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Song), songDb.Id);
        return _mapper.Map<SongDto>(songDb);
    }
}
