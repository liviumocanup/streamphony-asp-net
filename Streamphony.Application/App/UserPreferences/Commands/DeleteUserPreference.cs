using MediatR;
using Streamphony.Application.Abstractions;

namespace Streamphony.Application.App.UserPreferences.Commands;

public record DeleteUserPreference(Guid Id) : IRequest<bool>;

public class DeleteUserPreferenceHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserPreference, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteUserPreference request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserPreferenceRepository.GetById(request.Id) == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.UserPreferenceRepository.Delete(request.Id);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();

            Console.WriteLine(e.Message);
            return false;
        }

        return true;
    }
}