using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Genres.Responses;

namespace Streamphony.Application.App.Genres.Commands;

public record UpdateGenre(GenreDto GenreDto) : IRequest<GenreDto>;

public class UpdateGenreHandler(IUnitOfWork unitOfWork, IMappingProvider mapper) : IRequestHandler<UpdateGenre, GenreDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;

    public async Task<GenreDto> Handle(UpdateGenre request, CancellationToken cancellationToken)
    {
        var genreDto = request.GenreDto;

        var genre = await _unitOfWork.GenreRepository.GetById(genreDto.Id, cancellationToken) ??
                throw new KeyNotFoundException($"Genre with ID {genreDto.Id} not found.");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            _mapper.Map(genreDto, genre);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return _mapper.Map<GenreDto>(genre);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
