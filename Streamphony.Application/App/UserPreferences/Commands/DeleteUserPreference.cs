using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.UserPreferences.Commands;

public record DeleteUserPreference(Guid Id) : IRequest<bool>;

public class DeleteUserPreferenceHandler(IUnitOfWork unitOfWork, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<DeleteUserPreference, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<bool> Handle(DeleteUserPreference request, CancellationToken cancellationToken)
    {
        var userPreferenceId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.UserPreferenceRepository, userPreferenceId, cancellationToken);

        await _unitOfWork.UserPreferenceRepository.Delete(request.Id, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Delete, nameof(UserPreference), userPreferenceId);
        return true;
    }
}