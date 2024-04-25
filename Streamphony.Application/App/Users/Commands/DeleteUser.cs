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
        if (await _unitOfWork.UserRepository.GetById(userId) == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _unitOfWork.SongRepository.DeleteWhere(song => song.OwnerId == userId);
            await _unitOfWork.UserRepository.Delete(userId);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            await _loggingService.LogAsync($"User id {userId} - deleted successfully");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();

            await _loggingService.LogAsync($"Error deleting user id {userId}: ", ex);
            return false;
        }

        return true;
    }
}