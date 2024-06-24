using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Queries;

public record GetSongById(Guid Id) : IRequest<SongDetailsDto>;

public class GetSongByIdHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService) : IRequestHandler<GetSongById, SongDetailsDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<SongDetailsDto> Handle(GetSongById request, CancellationToken cancellationToken)
    {
        var song = await _unitOfWork.SongRepository.GetByIdWithBlobs(request.Id, cancellationToken);

        _logger.LogSuccess(nameof(Song), song.Id, LogAction.Get);
        return _mapper.Map<SongDetailsDto>(song);
    }
}
