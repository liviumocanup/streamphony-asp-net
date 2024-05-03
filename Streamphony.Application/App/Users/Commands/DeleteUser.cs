using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;

namespace Streamphony.Application.App.Users.Commands;

public record DeleteUser(Guid Id) : IRequest<bool>;

public class DeleteUserHandler(IUnitOfWork unitOfWork, ILoggingService loggingService) : IRequestHandler<DeleteUser, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingService _loggingService = loggingService;

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

            await _loggingService.LogAsync($"User id {userId} - deleted successfully");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Error deleting user id {userId}: ", ex);
            return false;
        }

        return true;
    }
}