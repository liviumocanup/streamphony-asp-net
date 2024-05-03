using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Genres.Commands;

public record CreateGenre(GenreCreationDto GenreCreationDto) : IRequest<GenreDto>;

public class CreateGenreHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateGenre, GenreDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<GenreDto> Handle(CreateGenre request, CancellationToken cancellationToken)
    {
        var genreEntity = _mapper.Map<Genre>(request.GenreCreationDto);

        if (await _unitOfWork.GenreRepository.GetByName(genreEntity.Name, cancellationToken) != null)
            throw new Exception($"Genre with name {genreEntity.Name} already exists");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var genreDb = await _unitOfWork.GenreRepository.Add(genreEntity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return _mapper.Map<GenreDto>(genreDb);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}