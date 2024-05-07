using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.App.Genres.Queries;

public class GetAllGenres() : IRequest<IEnumerable<GenreDto>>;

public class GetAllGenresHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllGenres, IEnumerable<GenreDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<IEnumerable<GenreDto>> Handle(GetAllGenres request, CancellationToken cancellationToken)
    {
        var genres = await _unitOfWork.GenreRepository.GetAll(cancellationToken);

        _logger.LogSuccess(nameof(Genre));
        return _mapper.Map<IEnumerable<GenreDto>>(genres);
    }
}