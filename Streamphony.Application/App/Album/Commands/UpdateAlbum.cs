using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;


namespace Streamphony.Application.App.Albums.Commands;

public record UpdateAlbum(AlbumDto AlbumDto) : IRequest<AlbumDto>;

public class UpdateAlbumHandler : IRequestHandler<UpdateAlbum, AlbumDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;
    private readonly IValidationService _validationService;

    public UpdateAlbumHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<AlbumDto> Handle(UpdateAlbum request, CancellationToken cancellationToken)
    {
        var albumDto = request.AlbumDto;
        var duplicateTitleForOtherAlbums = _unitOfWork.AlbumRepository.GetByOwnerIdAndTitleWhereIdNotEqual;

        var album = await _validationService.GetExistingEntity(_unitOfWork.AlbumRepository, albumDto.Id, cancellationToken);
        await ValidateOwnership(albumDto, cancellationToken);
        await _validationService.EnsureUserUniquePropertyExceptId(duplicateTitleForOtherAlbums, album.OwnerId, nameof(albumDto.Title), albumDto.Title, albumDto.Id, cancellationToken);

        _mapper.Map(albumDto, album);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id {EntityId}.", LogAction.Update, nameof(Album), album.Id);
        return _mapper.Map<AlbumDto>(album);
    }

    private async Task ValidateOwnership(AlbumDto albumDto, CancellationToken cancellationToken)
    {
        var user = await _validationService.GetExistingEntity(_unitOfWork.UserRepository, albumDto.OwnerId, cancellationToken, LogAction.Get);

        if (!user.OwnedAlbums.Any(album => album.Id == albumDto.Id))
        {
            _validationService.LogAndThrowNotAuthorizedException(nameof(Album), albumDto.Id, nameof(User), albumDto.OwnerId);
        }
    }
}
