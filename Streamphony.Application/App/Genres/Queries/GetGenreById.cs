using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.App.Genres.Responses;

namespace Streamphony.Application.App.Genres.Queries;

public record GetGenreById(Guid Id) : IRequest<GenreDetailsDto>;

public class GetGenreByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetGenreById, GenreDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<GenreDetailsDto> Handle(GetGenreById request, CancellationToken cancellationToken)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdWithInclude(request.Id, genre => genre.Songs);

        return _mapper.Map<GenreDetailsDto>(genre);
    }
}