using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Models;

namespace Streamphony.Application.App.Genres.Queries;

public class GetAllGenres(int pageNumber, int pageSize) : IRequest<PaginatedResult<GenreDto>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}

public class GetAllGenresHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllGenres, PaginatedResult<GenreDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<GenreDto>> Handle(GetAllGenres request, CancellationToken cancellationToken)
    {
        (IEnumerable<Genre> genres, int totalRecords) = await _unitOfWork.GenreRepository.GetAllPaginated(request.PageNumber, request.PageSize, cancellationToken);

        _logger.LogSuccess(nameof(Genre));
        return new PaginatedResult<GenreDto>(_mapper.Map<IEnumerable<GenreDto>>(genres), totalRecords);
    }
}