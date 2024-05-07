using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Albums.Commands;

public record DeleteAlbum(Guid Id) : IRequest<Unit>;

public class DeleteAlbumHandler(IUnitOfWork unitOfWork, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<DeleteAlbum, Unit>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<Unit> Handle(DeleteAlbum request, CancellationToken cancellationToken)
    {
        var albumdId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.AlbumRepository, albumdId, cancellationToken);

        await _unitOfWork.AlbumRepository.Delete(albumdId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Delete, nameof(Album), albumdId);
        return Unit.Value;
    }
}