using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common;

namespace Streamphony.Application.App.Genres.Queries;

public record GetAllGenres(PagedRequest PagedRequest) : IRequest<PaginatedResult<GenreDto>>;

public class GetAllGenresHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger) : IRequestHandler<GetAllGenres, PaginatedResult<GenreDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingService _logger = logger;

    public async Task<PaginatedResult<GenreDto>> Handle(GetAllGenres request, CancellationToken cancellationToken)
    {
        (var paginatedResult, var genreList) = await _unitOfWork.GenreRepository.GetAllPaginated<GenreDto>(request.PagedRequest, cancellationToken);

        paginatedResult.Items = _mapper.Map<IEnumerable<GenreDto>>(genreList);

        _logger.LogSuccess(nameof(Genre));
        return paginatedResult;
    }
}