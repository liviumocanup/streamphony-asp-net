using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Songs.Commands;

public record DeleteSong(Guid Id, Guid UserId) : IRequest<bool>;

public class DeleteSongHandler(
    IUnitOfWork unitOfWork,
    ILoggingService logger,
    IValidationService validationService,
    IUserManagerProvider userManagerProvider)
    : IRequestHandler<DeleteSong, bool>
{
    private readonly ILoggingService _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;

    public async Task<bool> Handle(DeleteSong request, CancellationToken cancellationToken)
    {
        var songId = request.Id;

        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, request.UserId, cancellationToken);
        var artistId = userDb.ArtistId;
        
        await _validationService.AssertNavigationEntityExists<User, Artist>(_unitOfWork.ArtistRepository, artistId, cancellationToken, isNavRequired: true);
        await _validationService.AssertEntityExists(_unitOfWork.SongRepository, songId, cancellationToken);

        await _unitOfWork.SongRepository.Delete(songId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Song), songId, LogAction.Delete);
        return true;
    }
}
