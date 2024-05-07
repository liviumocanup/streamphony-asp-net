using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Genres.Commands;

public record DeleteGenre(Guid Id) : IRequest<bool>;

public class DeleteGenreHandler(IUnitOfWork unitOfWork, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<DeleteGenre, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<bool> Handle(DeleteGenre request, CancellationToken cancellationToken)
    {
        var genreId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.GenreRepository, genreId, cancellationToken);

        await _unitOfWork.GenreRepository.Delete(request.Id, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Delete, nameof(Genre), genreId);
        return true;
    }
}