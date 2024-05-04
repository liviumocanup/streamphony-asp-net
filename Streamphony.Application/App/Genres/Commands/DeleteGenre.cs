using MediatR;
using Streamphony.Application.Abstractions;

namespace Streamphony.Application.App.Genres.Commands;

public record DeleteGenre(Guid Id) : IRequest<bool>;

public class DeleteGenreHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteGenre, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteGenre request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.GenreRepository.GetById(request.Id, cancellationToken) == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.GenreRepository.Delete(request.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            Console.WriteLine(e.Message);
            //TODO: Log error
            return false;
        }

        return true;
    }
}