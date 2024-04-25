using MediatR;
using Streamphony.Application.Abstractions;

namespace Streamphony.Application.App.Genres.Commands;

public record DeleteGenre(Guid Id) : IRequest<bool>;

public class DeleteGenreHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteGenre, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteGenre request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.GenreRepository.GetById(request.Id) == null) return false;

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.GenreRepository.Delete(request.Id);
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