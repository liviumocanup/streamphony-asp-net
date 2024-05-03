using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.App.Genres.Responses;

namespace Streamphony.Application.App.Genres.Commands;

public record UpdateGenre(GenreDto GenreDto) : IRequest<GenreDto>;

public class UpdateGenreHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateGenre, GenreDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

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
