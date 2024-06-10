using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record DeleteSong(Guid Id) : IRequest<bool>;

public class DeleteSongHandler(IUnitOfWork unitOfWork, ILoggingService logger, IValidationService validationService)
    : IRequestHandler<DeleteSong, bool>
{
    private readonly ILoggingService _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<bool> Handle(DeleteSong request, CancellationToken cancellationToken)
    {
        var songId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.SongRepository, songId, cancellationToken);

        await _unitOfWork.SongRepository.Delete(songId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Song), songId, LogAction.Delete);
        return true;
    }
}
