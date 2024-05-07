using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Services;

namespace Streamphony.Application.App.Genres.Commands;

public record UpdateGenre(GenreDto GenreDto) : IRequest<GenreDto>;

public class UpdateGenreHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<UpdateGenre, GenreDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<GenreDto> Handle(UpdateGenre request, CancellationToken cancellationToken)
    {
        var genreDto = request.GenreDto;
        var duplicateNameForOtherGenres = _unitOfWork.GenreRepository.GetByNameWhereIdNotEqual;

        var genre = await _validationService.GetExistingEntity(_unitOfWork.GenreRepository, genreDto.Id, cancellationToken);
        await _validationService.EnsureUniquePropertyExceptId(duplicateNameForOtherGenres, nameof(genreDto.Name), genreDto.Name, genreDto.Id, cancellationToken);

        _mapper.Map(genreDto, genre);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Update, nameof(Genre), genre.Id);
        return _mapper.Map<GenreDto>(genre);
    }
}
