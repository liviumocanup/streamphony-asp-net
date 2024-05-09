using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.Application.App.Albums.Commands;

public record CreateAlbum(AlbumCreationDto AlbumCreationDto) : IRequest<AlbumDto>;

public class CreateAlbumHandler : IRequestHandler<CreateAlbum, AlbumDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingService _logger;
    private readonly IValidationService _validationService;

    public CreateAlbumHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<AlbumDto> Handle(CreateAlbum request, CancellationToken cancellationToken)
    {
        var albumDto = request.AlbumCreationDto;
        var getByOwnerIdAndTitleFunc = _unitOfWork.AlbumRepository.GetByOwnerIdAndTitle;

        await _validationService.AssertNavigationEntityExists<Album, User>(_unitOfWork.UserRepository, albumDto.OwnerId, cancellationToken);
        await _validationService.EnsureUserUniqueProperty(getByOwnerIdAndTitleFunc, albumDto.OwnerId, nameof(albumDto.Title), albumDto.Title, cancellationToken);

        var albumEntity = _mapper.Map<Album>(albumDto);
        var albumDb = await _unitOfWork.AlbumRepository.Add(albumEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Album), albumDb.Id);
        return _mapper.Map<AlbumDto>(albumDb);
    }
}