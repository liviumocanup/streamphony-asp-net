using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Commands;

public record UpdateAlbum(AlbumDto AlbumDto) : IRequest<AlbumDto>;

public class UpdateAlbumHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService)
    : IRequestHandler<UpdateAlbum, AlbumDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<AlbumDto> Handle(UpdateAlbum request, CancellationToken cancellationToken)
    {
        var albumDto = request.AlbumDto;
        var duplicateTitleForOtherAlbums = _unitOfWork.AlbumRepository.GetByOwnerIdAndTitleWhereIdNotEqual;

        var album = await _validationService.GetExistingEntity(_unitOfWork.AlbumRepository, albumDto.Id,
            cancellationToken);
        await ValidateOwnership(albumDto, cancellationToken);
        await _validationService.EnsureArtistUniquePropertyExceptId(duplicateTitleForOtherAlbums, album.OwnerId,
            nameof(albumDto.Title), albumDto.Title, albumDto.Id, cancellationToken);

        _mapper.Map(albumDto, album);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Album), album.Id, LogAction.Update);
        return _mapper.Map<AlbumDto>(album);
    }

    private async Task ValidateOwnership(AlbumDto albumDto, CancellationToken cancellationToken)
    {
        // var artist = await _validationService.GetExistingEntity(_unitOfWork.ArtistRepository, albumDto.OwnerId,
        //     cancellationToken, LogAction.Get);
        //
        // if (artist.OwnedAlbums.All(album => album.Id != albumDto.Id))
        //     _logger.LogAndThrowNotAuthorizedException(nameof(Album), albumDto.Id, nameof(Artist), albumDto.OwnerId);
    }
}
