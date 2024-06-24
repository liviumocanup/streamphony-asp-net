using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Genres.Queries;

public record GetAllGenres(PagedRequest PagedRequest) : IRequest<PaginatedResult<GenreDto>>;

public class GetAllGenresHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger)
    : IRequestHandler<GetAllGenres, PaginatedResult<GenreDto>>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<GenreDto>> Handle(GetAllGenres request, CancellationToken cancellationToken)
    {
        var (paginatedResult, genreList) =
            await _unitOfWork.GenreRepository.GetAllPaginated<GenreDto>(request.PagedRequest, cancellationToken);

        paginatedResult.Items = _mapper.Map<IEnumerable<GenreDto>>(genreList);

        _logger.LogSuccess(nameof(Genre));
        return paginatedResult;
    }
}
