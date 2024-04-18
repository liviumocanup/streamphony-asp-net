using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
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

        if (await _unitOfWork.GenreRepository.GetByName(genreEntity.Name) != null)
            throw new Exception($"Genre with name {genreEntity.Name} already exists");

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var genreDb = await _unitOfWork.GenreRepository.Add(genreEntity);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<GenreDto>(genreDb);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}