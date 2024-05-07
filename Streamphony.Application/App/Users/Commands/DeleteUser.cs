using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Users.Commands;

public record DeleteUser(Guid Id) : IRequest<bool>;

public class DeleteUserHandler(IUnitOfWork unitOfWork, ILoggingService logger, IValidationService validationService) : IRequestHandler<DeleteUser, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingService _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        Guid userId = request.Id;
        await _validationService.AssertEntityExists(_unitOfWork.UserRepository, userId, cancellationToken);

        await _unitOfWork.SongRepository.DeleteWhere(song => song.OwnerId == userId, cancellationToken);
        await _unitOfWork.UserRepository.Delete(userId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(User), userId, LogAction.Delete);
        return true;
    }
}