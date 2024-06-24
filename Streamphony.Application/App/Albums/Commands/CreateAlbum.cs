using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Commands;

public record CreateAlbum(AlbumCreationDto AlbumCreationDto, Guid UserId) : IRequest<AlbumDto>;

public class CreateAlbumHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider)
    : IRequestHandler<CreateAlbum, AlbumDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<AlbumDto> Handle(CreateAlbum request, CancellationToken cancellationToken)
    {
        var albumCreationDto = request.AlbumCreationDto;
        var userId = request.UserId;
        var getByOwnerIdAndTitleFunc = _unitOfWork.AlbumRepository.GetByOwnerIdAndTitle;

        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, userId, cancellationToken);
        var artistId = userDb.ArtistId;

        var coverBlob = await _validationService.GetExistingEntity(_unitOfWork.BlobRepository,
            albumCreationDto.CoverFileId,
            cancellationToken, LogAction.Create);
        await _validationService.AssertNavigationEntityExists<Album, Artist>(_unitOfWork.ArtistRepository,
            artistId, cancellationToken, isNavRequired: true);
        await _validationService.EnsureArtistUniqueProperty(getByOwnerIdAndTitleFunc, artistId!.Value,
            nameof(albumCreationDto.Title), albumCreationDto.Title, cancellationToken);

        var songs = await ValidateSongs(albumCreationDto.SongIds, cancellationToken);

        var albumEntity = _mapper.Map<Album>(albumCreationDto);
        albumEntity.OwnerId = artistId.Value;
        albumEntity.CoverBlob = coverBlob;
        albumEntity.Songs = songs;
        albumEntity.TotalDuration = albumEntity.CalculateTotalDuration();
        albumEntity.Collaborators = [];

        var albumDb = await _unitOfWork.AlbumRepository.Add(albumEntity, cancellationToken);
        
        await ValidateCollaborators(albumCreationDto.Collaborators, albumEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        _logger.LogSuccess(nameof(Album), albumDb.Id);
        return _mapper.Map<AlbumDto>(albumDb);
    }

    private async Task<List<Song>> ValidateSongs(HashSet<Guid> songIds, CancellationToken cancellationToken)
    {
        if (songIds.Count == 0) throw new ArgumentException("Album must have at least one song.");
        var songs = await _unitOfWork.SongRepository.GetByIdsWithBlobs(songIds, cancellationToken);
        var enumerableSong = songs as Song[] ?? songs.ToArray();
        if (enumerableSong.Length != songIds.Count)
            throw new ArgumentException("Some songs do not exist.");

        foreach (var song in enumerableSong)
        {
            if (song.AlbumId != null)
                throw new ArgumentException("Some songs are already in an album.");
        }

        return enumerableSong.ToList();
    }

    private async Task ValidateCollaborators(Dictionary<Guid, ArtistRole[]>? collaborators, Album album,
        CancellationToken cancellationToken)
    {
        if (collaborators == null) return;

        foreach (var collaborator in collaborators)
        {
            var artist = await _validationService.GetExistingEntity(_unitOfWork.ArtistRepository, collaborator.Key,
                cancellationToken);
            
            if (collaborator.Value.Length > 3)
                throw new ArgumentException("Collaborator cannot have more than three roles.");
            
            foreach (var role in collaborator.Value)
            {
                var albumArtist = new AlbumArtist
                {
                    AlbumId = album.Id,
                    ArtistId = artist.Id,
                    Role = role
                };
                await _unitOfWork.AlbumArtistRepository.Add(albumArtist, cancellationToken);
            }
        }
    }
}
