using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Commands;

public record DeleteAlbum(Guid Id) : IRequest<Unit>;

public class DeleteAlbumHandler(IUnitOfWork unitOfWork, ILoggingService logger, IValidationService validationService)
    : IRequestHandler<DeleteAlbum, Unit>
{
    private readonly ILoggingService _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<Unit> Handle(DeleteAlbum request, CancellationToken cancellationToken)
    {
        var albumId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.AlbumRepository, albumId, cancellationToken);

        await _unitOfWork.AlbumRepository.Delete(albumId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Album), albumId, LogAction.Delete);
        return Unit.Value;
    }
}
