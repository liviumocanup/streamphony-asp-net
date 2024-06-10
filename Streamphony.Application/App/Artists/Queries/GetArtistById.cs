using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Artists.Queries;

public record GetArtistById(Guid Id) : IRequest<ArtistDetailsDto>;

public class GetArtistByIdHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService) : IRequestHandler<GetArtistById, ArtistDetailsDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<ArtistDetailsDto> Handle(GetArtistById request, CancellationToken cancellationToken)
    {
        var artist = await _validationService.GetExistingEntityWithInclude<Artist>(
            _unitOfWork.ArtistRepository.GetByIdWithInclude,
            request.Id,
            LogAction.Get,
            cancellationToken,
            artist => artist.UploadedSongs,
            artist => artist.Preference,
            artist => artist.OwnedAlbums
        );

        _logger.LogSuccess(nameof(Artist), artist.Id, LogAction.Get);
        return _mapper.Map<ArtistDetailsDto>(artist);
    }
}
