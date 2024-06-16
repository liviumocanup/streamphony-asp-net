using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.Responses;
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
        var artistDto = request.ArtistCreationDto;
        var userId = request.UserId;
        var getByIdFunc = _unitOfWork.ArtistRepository.FindByUserIdAsync;

        var userDb = await _userManagerProvider.FindByIdAsync(userId.ToString());
        if (userDb == null)
            _logger.LogAndThrowNotFoundExceptionForNavigation(nameof(Artist), nameof(User), userId);
        await _validationService.EnsureUniqueProperty(getByIdFunc, nameof(User), userId, cancellationToken);

        var artistEntity = _mapper.Map<Artist>(artistDto);
        artistEntity.UserId = userId;

        var artistDb = await _unitOfWork.ArtistRepository.Add(artistEntity, cancellationToken);
        userDb!.ArtistId = artistDb.Id;
        await _unitOfWork.SaveAsync(cancellationToken);
        
        _logger.LogSuccess(nameof(Artist), artistDb.Id);
        return _mapper.Map<ArtistDto>(artistDb);
    }
}
