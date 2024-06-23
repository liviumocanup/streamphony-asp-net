using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Artists.Commands;

public record CreateArtist(ArtistCreationDto ArtistCreationDto, Guid UserId) : IRequest<ArtistDto>;

public class CreateArtistHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IUserManagerProvider userManagerProvider,
    IValidationService validationService)
    : IRequestHandler<CreateArtist, ArtistDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;
    private readonly IValidationService _validationService = validationService;

    public async Task<ArtistDto> Handle(CreateArtist request, CancellationToken cancellationToken)
    {
        var artistCreationDto = request.ArtistCreationDto;
        var userId = request.UserId;
        var getByIdFunc = _unitOfWork.ArtistRepository.FindByUserIdAsync;

        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, userId, cancellationToken);
        await _validationService.EnsureUniqueProperty(getByIdFunc, nameof(User), userId, cancellationToken);
        
        var pfpBlob = await _validationService.GetExistingEntity(_unitOfWork.BlobRepository, artistCreationDto.ProfilePictureId,
            cancellationToken, LogAction.Create);

        var artistEntity = _mapper.Map<Artist>(artistCreationDto);
        artistEntity.UserId = userId;
        artistEntity.ProfilePictureBlob = pfpBlob;

        var artistDb = await _unitOfWork.ArtistRepository.Add(artistEntity, cancellationToken);
        userDb.ArtistId = artistDb.Id;
        await _unitOfWork.SaveAsync(cancellationToken);
        
        _logger.LogSuccess(nameof(Artist), artistDb.Id);
        return _mapper.Map<ArtistDto>(artistDb);
    }
}
