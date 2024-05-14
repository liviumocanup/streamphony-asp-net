using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Preferences.Commands;

public record DeletePreference(Guid Id) : IRequest<bool>;

public class DeletePreferenceHandler(IUnitOfWork unitOfWork, ILoggingService logger, IValidationService validationService) : IRequestHandler<DeletePreference, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingService _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<bool> Handle(DeletePreference request, CancellationToken cancellationToken)
    {
        var preferenceId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.PreferenceRepository, preferenceId, cancellationToken);

        await _unitOfWork.PreferenceRepository.Delete(request.Id, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Preference), preferenceId, LogAction.Delete);
        return true;
    }
}