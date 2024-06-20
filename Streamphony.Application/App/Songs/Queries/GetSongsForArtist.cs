using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Songs.Queries;

public record GetSongForArtist(Guid Id) : IRequest<IEnumerable<SongResponseDto>>;

public class GetSongsForArtistHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider) : IRequestHandler<GetSongForArtist, IEnumerable<SongResponseDto>>
{
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<IEnumerable<SongResponseDto>> Handle(GetSongForArtist request, CancellationToken cancellationToken)
    {
        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, request.Id, cancellationToken);
        var artistId = userDb.ArtistId;
        await _validationService.AssertNavigationEntityExists<User, Artist>(_unitOfWork.ArtistRepository, artistId, cancellationToken, isNavRequired: true);

        var songs = await _unitOfWork.SongRepository.GetByOwnerIdWithBlobs(artistId!.Value, cancellationToken);

        return _mapper.Map<IEnumerable<SongResponseDto>>(songs);
    }
}
