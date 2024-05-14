using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.Responses;

namespace Streamphony.Application.App.Artists.Commands;

public record CreateArtist(ArtistCreationDto ArtistCreationDto) : IRequest<ArtistDto>;

public class CreateArtistHandler : IRequestHandler<CreateArtist, ArtistDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingService _logger;
    public CreateArtistHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

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