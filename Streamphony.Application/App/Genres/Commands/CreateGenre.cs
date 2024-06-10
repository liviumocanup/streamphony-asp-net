using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Genres.Commands;

public record CreateGenre(GenreCreationDto GenreCreationDto) : IRequest<GenreDto>;

public class CreateGenreHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService) : IRequestHandler<CreateGenre, GenreDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<GenreDto> Handle(CreateGenre request, CancellationToken cancellationToken)
    {
        var genreDto = request.GenreCreationDto;
        var getByNameFunc = _unitOfWork.GenreRepository.GetByName;

        await _validationService.EnsureUniqueProperty(getByNameFunc, nameof(genreDto.Name), genreDto.Name,
            cancellationToken);

        var genreEntity = _mapper.Map<Genre>(request.GenreCreationDto);
        var genreDb = await _unitOfWork.GenreRepository.Add(genreEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Genre), genreDb.Id);
        return _mapper.Map<GenreDto>(genreDb);
    }
}
