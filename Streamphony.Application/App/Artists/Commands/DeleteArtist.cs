using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Artists.Commands;

public record DeleteArtist(Guid Id, Guid UserId) : IRequest<bool>;

public class DeleteArtistHandler(
    IUnitOfWork unitOfWork,
    ILoggingService logger,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider)
    : IRequestHandler<DeleteArtist, bool>
{
    private readonly ILoggingService _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<bool> Handle(DeleteArtist request, CancellationToken cancellationToken)
    {
        var artistId = request.Id;
        var userId = request.UserId;
        
        var userDb = await _userManagerProvider.FindByIdAsync(userId.ToString());
        await _validationService.AssertEntityExists(_unitOfWork.ArtistRepository, artistId, cancellationToken);

        await _unitOfWork.SongRepository.DeleteWhere(song => song.OwnerId == artistId, cancellationToken);
        await _unitOfWork.ArtistRepository.Delete(artistId, cancellationToken);
        userDb!.ArtistId = null;
        
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Artist), artistId, LogAction.Delete);
        return true;
    }
}
