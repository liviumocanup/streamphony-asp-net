using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Artists.Queries;

public record GetArtistForUser(Guid Id) : IRequest<ArtistDetailsDto>;

public class GetArtistForUserHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider) : IRequestHandler<GetArtistForUser, ArtistDetailsDto>
{
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<ArtistDetailsDto> Handle(GetArtistForUser request, CancellationToken cancellationToken)
    {
        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, request.Id, cancellationToken);
        var artistId = userDb.ArtistId;
        await _validationService.AssertNavigationEntityExists<User, Artist>(_unitOfWork.ArtistRepository, artistId, cancellationToken, isNavRequired: true);
        
        var artist = await _unitOfWork.ArtistRepository.GetByOwnerIdWithBlobs(userDb.Id, cancellationToken);
        return _mapper.Map<ArtistDetailsDto>(artist);
    }
}
