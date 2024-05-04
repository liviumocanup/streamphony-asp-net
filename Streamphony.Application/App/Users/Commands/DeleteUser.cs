using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Commands;

public record DeleteUser(Guid Id) : IRequest<bool>;

public class DeleteUserHandler(IUnitOfWork unitOfWork, ILoggingProvider logger) : IRequestHandler<DeleteUser, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingProvider _logger = logger;

    public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        Guid userId = request.Id;
        if (await _unitOfWork.UserRepository.GetById(userId, cancellationToken) == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.SongRepository.DeleteWhere(song => song.OwnerId == userId, cancellationToken);
            await _unitOfWork.UserRepository.Delete(userId, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            _logger.LogInformation("Successfully deleted {EntityType} with Id {EntityId}.", nameof(User), userId);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogError("Failed to delete {EntityType}. Error: {Error}", nameof(User), ex.ToString());
            return false;
        }

        return true;
    }
}