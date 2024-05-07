using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Genres.Responses;

namespace Streamphony.Application.App.Genres.Queries;

public class GetAllGenres() : IRequest<IEnumerable<GenreDto>>;

public class GetAllGenresHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger) : IRequestHandler<GetAllGenres, IEnumerable<GenreDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;

    public async Task<IEnumerable<GenreDto>> Handle(GetAllGenres request, CancellationToken cancellationToken)
    {
        var genres = await _unitOfWork.GenreRepository.GetAll(cancellationToken);

        _logger.LogInformation("Retrieved all {EntityType}s.", nameof(Genre));
        return _mapper.Map<IEnumerable<GenreDto>>(genres);
    }
}