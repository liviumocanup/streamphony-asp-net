using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Songs.Commands;

public record UpdateSong(SongDto SongDto) : IRequest<SongDto>;

public class UpdateSongHandler : IRequestHandler<UpdateSong, SongDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;
    private readonly IValidationService _validationService;

    public UpdateSongHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<SongDto> Handle(UpdateSong request, CancellationToken cancellationToken)
    {
        var songDto = request.SongDto;
        var duplicateTitleForOtherSongs = _unitOfWork.SongRepository.GetByOwnerIdAndTitleWhereIdNotEqual;

        var song = await _validationService.GetExistingEntity(_unitOfWork.SongRepository, songDto.Id, cancellationToken);
        await ValidateOwnership(songDto, cancellationToken);
        await _validationService.AssertNavigationEntityExists<Song, Genre>(_unitOfWork.GenreRepository, songDto.GenreId, cancellationToken, LogAction.Update);
        await _validationService.AssertNavigationEntityExists<Song, Album>(_unitOfWork.AlbumRepository, songDto.AlbumId, cancellationToken, LogAction.Update);
        await _validationService.EnsureUserUniquePropertyExceptId(duplicateTitleForOtherSongs, songDto.OwnerId, nameof(songDto.Title), songDto.Title, songDto.Id, cancellationToken);

        _mapper.Map(songDto, song);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Update, nameof(Song), song.Id);
        return _mapper.Map<SongDto>(song);
    }

    private async Task ValidateOwnership(SongDto songDto, CancellationToken cancellationToken)
    {
        var user = await _validationService.GetExistingEntity(_unitOfWork.UserRepository, songDto.OwnerId, cancellationToken, LogAction.Get);

        if (!user.UploadedSongs.Any(song => song.Id == songDto.Id))
        {
            _validationService.LogAndThrowNotAuthorizedException(nameof(Song), songDto.Id, nameof(User), songDto.OwnerId);
        }
    }
}
