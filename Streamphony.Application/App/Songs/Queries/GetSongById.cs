using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Songs.Queries;

public record GetSongById(Guid Id) : IRequest<SongDto>;

public class GetSongByIdHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<GetSongById, SongDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<SongDto> Handle(GetSongById request, CancellationToken cancellationToken)
    {
        var song = await _validationService.GetExistingEntity(_unitOfWork.SongRepository, request.Id, cancellationToken, LogAction.Get);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Get, nameof(Song), song.Id);
        return _mapper.Map<SongDto>(song);
    }
}