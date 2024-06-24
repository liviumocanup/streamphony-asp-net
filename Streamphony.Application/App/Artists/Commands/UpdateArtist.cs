using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Artists.Commands;

public record UpdateArtist(ArtistDto ArtistDto) : IRequest<ArtistDto>;

public class UpdateArtistHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService)
    : IRequestHandler<UpdateArtist, ArtistDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<ArtistDto> Handle(UpdateArtist request, CancellationToken cancellationToken)
    {
        var artistDto = request.ArtistDto;
        var artist =
            await _validationService.GetExistingEntity(_unitOfWork.ArtistRepository, artistDto.Id, cancellationToken);

        _mapper.Map(artistDto, artist);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Artist), artist.Id, LogAction.Update);
        return _mapper.Map<ArtistDto>(artist);
    }
}
