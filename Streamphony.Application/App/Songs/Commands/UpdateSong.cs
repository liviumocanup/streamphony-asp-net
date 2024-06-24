using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record UpdateSong(SongEditRequestDto SongDto, Guid UserId) : IRequest<SongDto>;

public class UpdateSongHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService)
    : IRequestHandler<UpdateSong, SongDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<SongDto> Handle(UpdateSong request, CancellationToken cancellationToken)
    {
        var songDto = request.SongDto;
        var userId = request.UserId;
        var duplicateTitleForOtherSongs = _unitOfWork.SongRepository.GetByOwnerIdAndTitleWhereIdNotEqual;
        
        var songDb = await _validationService.GetExistingEntity(_unitOfWork.SongRepository, songDto.Id,
            cancellationToken);
        await ValidateOwnership(songDto, userId, cancellationToken);
        await _validationService.EnsureArtistUniquePropertyExceptId(duplicateTitleForOtherSongs, userId,
            nameof(songDto.Title), songDto.Title, songDto.Id, cancellationToken);

        _mapper.Map(songDto, songDb);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Song), songDb.Id, LogAction.Update);
        return _mapper.Map<SongDto>(songDb);
    }

    private async Task ValidateOwnership(SongEditRequestDto songDto, Guid userId, CancellationToken cancellationToken)
    {
        var artist = await _unitOfWork.ArtistRepository.FindByUserIdAsync(userId, cancellationToken);

        if (artist!.UploadedSongs.All(song => song.Id != songDto.Id))
            _logger.LogAndThrowNotAuthorizedException(nameof(Song), songDto.Id, nameof(Artist), userId);
    }
}
