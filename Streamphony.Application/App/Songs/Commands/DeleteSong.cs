using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Songs.Commands;

public record DeleteSong(Guid Id) : IRequest<bool>;

public class DeleteSongHandler(IUnitOfWork unitOfWork, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<DeleteSong, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<bool> Handle(DeleteSong request, CancellationToken cancellationToken)
    {
        var songId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.SongRepository, songId, cancellationToken);

        await _unitOfWork.SongRepository.Delete(songId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Delete, nameof(Song), songId);
        return true;
    }
}