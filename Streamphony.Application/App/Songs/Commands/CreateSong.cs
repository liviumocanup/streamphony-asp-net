using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.App.Songs.Commands;

public record CreateSong(SongCreationDto SongCreationDto) : IRequest<SongDto>;

public class CreateSongHandler : IRequestHandler<CreateSong, SongDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingService _logger;
    private readonly IValidationService _validationService;

    public CreateSongHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<SongDto> Handle(CreateSong request, CancellationToken cancellationToken)
    {
        var songDto = request.SongCreationDto;
        var getByOwnerIdAndTitleFunc = _unitOfWork.SongRepository.GetByOwnerIdAndTitle;

        await _validationService.AssertNavigationEntityExists<Song, User>(_unitOfWork.UserRepository, songDto.OwnerId, cancellationToken);
        await _validationService.AssertNavigationEntityExists<Song, Genre>(_unitOfWork.GenreRepository, songDto.GenreId, cancellationToken);
        await _validationService.AssertNavigationEntityExists<Song, Album>(_unitOfWork.AlbumRepository, songDto.AlbumId, cancellationToken);
        await _validationService.EnsureUserUniqueProperty(getByOwnerIdAndTitleFunc, songDto.OwnerId, nameof(songDto.Title), songDto.Title, cancellationToken);

        var songEntity = _mapper.Map<Song>(songDto);
        var songDb = await _unitOfWork.SongRepository.Add(songEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Song), songDb.Id);
        return _mapper.Map<SongDto>(songDb);
    }
}