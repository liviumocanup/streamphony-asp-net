using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Genres.Commands;

public record DeleteGenre(Guid Id) : IRequest<bool>;

public class DeleteGenreHandler(IUnitOfWork unitOfWork, ILoggingService logger, IValidationService validationService)
    : IRequestHandler<DeleteGenre, bool>
{
    private readonly ILoggingService _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<bool> Handle(DeleteGenre request, CancellationToken cancellationToken)
    {
        var genreId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.GenreRepository, genreId, cancellationToken);

        await _unitOfWork.GenreRepository.Delete(request.Id, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Genre), genreId, LogAction.Delete);
        return true;
    }
}
