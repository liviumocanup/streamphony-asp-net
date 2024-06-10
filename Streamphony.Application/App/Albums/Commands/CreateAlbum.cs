using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Commands;

public record CreateAlbum(AlbumCreationDto AlbumCreationDto) : IRequest<AlbumDto>;

public class CreateAlbumHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService)
    : IRequestHandler<CreateAlbum, AlbumDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<AlbumDto> Handle(CreateAlbum request, CancellationToken cancellationToken)
    {
        var albumDto = request.AlbumCreationDto;
        var getByOwnerIdAndTitleFunc = _unitOfWork.AlbumRepository.GetByOwnerIdAndTitle;

        await _validationService.AssertNavigationEntityExists<Album, Artist>(_unitOfWork.ArtistRepository,
            albumDto.OwnerId, cancellationToken);
        await _validationService.EnsureArtistUniqueProperty(getByOwnerIdAndTitleFunc, albumDto.OwnerId,
            nameof(albumDto.Title), albumDto.Title, cancellationToken);

        var albumEntity = _mapper.Map<Album>(albumDto);
        var albumDb = await _unitOfWork.AlbumRepository.Add(albumEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Album), albumDb.Id);
        return _mapper.Map<AlbumDto>(albumDb);
    }
}
