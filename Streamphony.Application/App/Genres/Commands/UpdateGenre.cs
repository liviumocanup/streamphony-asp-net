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

        var genre = await _unitOfWork.GenreRepository.GetById(genreDto.Id) ??
                throw new KeyNotFoundException($"Genre with ID {genreDto.Id} not found.");

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _mapper.Map(genreDto, genre);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<GenreDto>(genre);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
