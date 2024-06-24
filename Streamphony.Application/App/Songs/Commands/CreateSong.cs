using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record CreateSong(SongCreationDto SongCreationDto, Guid UserId) : IRequest<SongDto>;

public class CreateSongHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider)
    : IRequestHandler<CreateSong, SongDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<SongDto> Handle(CreateSong request, CancellationToken cancellationToken)
    {
        var songCreationDto = request.SongCreationDto;
        var userId = request.UserId;
        var getByOwnerIdAndTitleFunc = _unitOfWork.SongRepository.GetByOwnerIdAndTitle;

        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, userId, cancellationToken);
        var artistId = userDb.ArtistId;
        
        await _validationService.AssertNavigationEntityExists<Song, Artist>(_unitOfWork.ArtistRepository,
            artistId, cancellationToken, isNavRequired: true);
        await _validationService.AssertNavigationEntityExists<Song, Genre>(_unitOfWork.GenreRepository,
            songCreationDto.GenreId, cancellationToken);
        await _validationService.AssertNavigationEntityExists<Song, Album>(_unitOfWork.AlbumRepository,
            songCreationDto.AlbumId, cancellationToken);
        await _validationService.EnsureArtistUniqueProperty(getByOwnerIdAndTitleFunc, artistId!.Value,
            nameof(songCreationDto.Title), songCreationDto.Title, cancellationToken);
        
        var coverBlob = await _validationService.GetExistingEntity(_unitOfWork.BlobRepository, songCreationDto.CoverFileId,
            cancellationToken, LogAction.Create);
        var songBlob = await _validationService.GetExistingEntity(_unitOfWork.BlobRepository, songCreationDto.AudioFileId,
            cancellationToken, LogAction.Create);
        
        var songEntity = _mapper.Map<Song>(songCreationDto);
        songEntity.OwnerId = artistId.Value;
        songEntity.CoverBlob = coverBlob;
        songEntity.AudioBlob = songBlob;
        songEntity.Duration = songBlob.Duration!.Value;
        
        var songDb = await _unitOfWork.SongRepository.Add(songEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        _logger.LogSuccess(nameof(Song), songDb.Id);
        return _mapper.Map<SongDto>(songDb);
    }
}
