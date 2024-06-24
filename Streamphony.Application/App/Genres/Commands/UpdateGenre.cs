using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Genres.Commands;

public record UpdateGenre(GenreDto GenreDto) : IRequest<GenreDto>;

public class UpdateGenreHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService) : IRequestHandler<UpdateGenre, GenreDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<GenreDto> Handle(UpdateGenre request, CancellationToken cancellationToken)
    {
        var genreDto = request.GenreDto;
        var duplicateNameForOtherGenres = _unitOfWork.GenreRepository.GetByNameWhereIdNotEqual;

        var genre = await _validationService.GetExistingEntity(_unitOfWork.GenreRepository, genreDto.Id,
            cancellationToken);
        await _validationService.EnsureUniquePropertyExceptId(duplicateNameForOtherGenres, nameof(genreDto.Name),
            genreDto.Name, genreDto.Id, cancellationToken);

        _mapper.Map(genreDto, genre);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Genre), genre.Id, LogAction.Update);
        return _mapper.Map<GenreDto>(genre);
    }
}
