using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Artists.Commands;

public record UpdateArtist(ArtistDto ArtistDto) : IRequest<ArtistDto>;

public class UpdateArtistHandler : IRequestHandler<UpdateArtist, ArtistDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingService _logger;
    private readonly IValidationService _validationService;

    public UpdateArtistHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<ArtistDto> Handle(UpdateArtist request, CancellationToken cancellationToken)
    {
        var artistDto = request.ArtistDto;
        var artist = await _validationService.GetExistingEntity(_unitOfWork.ArtistRepository, artistDto.Id, cancellationToken);

        _mapper.Map(artistDto, artist);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Artist), artist.Id, LogAction.Update);
        return _mapper.Map<ArtistDto>(artist);
    }
}
