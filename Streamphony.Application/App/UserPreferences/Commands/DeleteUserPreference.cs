using MediatR;
using Streamphony.Application.Abstractions;

namespace Streamphony.Application.App.UserPreferences.Commands;

public record DeleteUserPreference(Guid Id) : IRequest<bool>;

public class DeleteUserPreferenceHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserPreference, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteUserPreference request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserPreferenceRepository.GetById(request.Id, cancellationToken) == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.UserPreferenceRepository.Delete(request.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            Console.WriteLine(e.Message);
            return false;
        }

        return true;
    }
}