using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Albums.Queries;

public record GetAlbumsForUser(Guid Id) : IRequest<IEnumerable<AlbumDetailsDto>>;

public class GetAlbumsForUserHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider
    ) : IRequestHandler<GetAlbumsForUser, IEnumerable<AlbumDetailsDto>>
{
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<IEnumerable<AlbumDetailsDto>> Handle(GetAlbumsForUser request, CancellationToken cancellationToken)
    {
        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, request.Id, cancellationToken);
        var artistId = userDb.ArtistId;
        await _validationService.AssertNavigationEntityExists<User, Artist>(_unitOfWork.ArtistRepository, artistId, cancellationToken, isNavRequired: true);

        var albums = await _unitOfWork.AlbumRepository.GetByOwnerIdWithBlobs(artistId!.Value, cancellationToken);

        return _mapper.Map<IEnumerable<AlbumDetailsDto>>(albums);
    }
}
