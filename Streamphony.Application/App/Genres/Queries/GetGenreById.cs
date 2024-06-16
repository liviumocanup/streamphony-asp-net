using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Genres.Queries;

public record GetGenreById(Guid Id) : IRequest<GenreDetailsDto>;

public class GetGenreByIdHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService) : IRequestHandler<GetGenreById, GenreDetailsDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<GenreDetailsDto> Handle(GetGenreById request, CancellationToken cancellationToken)
    {
        var genre = await _validationService.GetExistingEntityWithInclude<Genre>(
            _unitOfWork.GenreRepository.GetByIdWithInclude,
            request.Id,
            LogAction.Get,
            cancellationToken,
            genre => genre.Songs
        );

        _logger.LogSuccess(nameof(Genre), genre.Id, LogAction.Get);
        return _mapper.Map<GenreDetailsDto>(genre);
    }
}
