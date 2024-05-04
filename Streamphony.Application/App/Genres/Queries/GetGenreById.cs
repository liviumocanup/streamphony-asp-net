using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Genres.Responses;

namespace Streamphony.Application.App.Genres.Queries;

public record GetGenreById(Guid Id) : IRequest<GenreDetailsDto>;

public class GetGenreByIdHandler(IUnitOfWork unitOfWork, IMappingProvider mapper) : IRequestHandler<GetGenreById, GenreDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;

    public async Task<GenreDetailsDto> Handle(GetGenreById request, CancellationToken cancellationToken)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdWithInclude(request.Id, cancellationToken, genre => genre.Songs);

        return _mapper.Map<GenreDetailsDto>(genre);
    }
}