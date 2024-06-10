using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Artists.Commands;

public record CreateArtist(ArtistCreationDto ArtistCreationDto) : IRequest<ArtistDto>;

public class CreateArtistHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger)
    : IRequestHandler<CreateArtist, ArtistDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ArtistDto> Handle(CreateArtist request, CancellationToken cancellationToken)
    {
        var artistDto = request.ArtistCreationDto;

        var artistEntity = _mapper.Map<Artist>(artistDto);
        var artistDb = await _unitOfWork.ArtistRepository.Add(artistEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Artist), artistDb.Id);
        return _mapper.Map<ArtistDto>(artistDb);
    }
}
