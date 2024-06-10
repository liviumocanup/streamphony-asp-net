using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Artists.Commands;

public record DeleteArtist(Guid Id) : IRequest<bool>;

public class DeleteArtistHandler(IUnitOfWork unitOfWork, ILoggingService logger, IValidationService validationService)
    : IRequestHandler<DeleteArtist, bool>
{
    private readonly ILoggingService _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<bool> Handle(DeleteArtist request, CancellationToken cancellationToken)
    {
        var artistId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.ArtistRepository, artistId, cancellationToken);

        await _unitOfWork.SongRepository.DeleteWhere(song => song.OwnerId == artistId, cancellationToken);
        await _unitOfWork.ArtistRepository.Delete(artistId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Artist), artistId, LogAction.Delete);
        return true;
    }
}
