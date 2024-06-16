using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record CreateSong(SongCreationDto SongCreationDto, Guid UserId) : IRequest<Guid>;

public class CreateSongHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider)
    : IRequestHandler<CreateSong, Guid>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<Guid> Handle(CreateSong request, CancellationToken cancellationToken)
    {
        var songDto = request.SongCreationDto;
        var getByOwnerIdAndTitleFunc = _unitOfWork.SongRepository.GetByOwnerIdAndTitle;
        
        var userDb = await _userManagerProvider.FindByIdAsync(request.UserId.ToString());
        var artistId = userDb!.ArtistId;

        await _validationService.AssertNavigationEntityExists<Song, Artist>(_unitOfWork.ArtistRepository,
            artistId, cancellationToken);
        await _validationService.AssertNavigationEntityExists<Song, Genre>(_unitOfWork.GenreRepository, songDto.GenreId,
            cancellationToken);
        await _validationService.AssertNavigationEntityExists<Song, Album>(_unitOfWork.AlbumRepository, songDto.AlbumId,
            cancellationToken);
        await _validationService.EnsureArtistUniqueProperty(getByOwnerIdAndTitleFunc, artistId!.Value,
            nameof(songDto.Title), songDto.Title, cancellationToken);

        var songEntity = _mapper.Map<Song>(songDto);
        songEntity.OwnerId = artistId.Value;
        var songDb = await _unitOfWork.SongRepository.Add(songEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Song), songDb.Id);
        return songDb.Id;
    }
}
