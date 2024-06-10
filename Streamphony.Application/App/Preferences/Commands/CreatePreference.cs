using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Preferences.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Preferences.Commands;

public record CreatePreference(PreferenceDto PreferenceDto) : IRequest<PreferenceDto>;

public class CreatePreferenceHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService)
    : IRequestHandler<CreatePreference, PreferenceDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<PreferenceDto> Handle(CreatePreference request, CancellationToken cancellationToken)
    {
        var preferenceDto = request.PreferenceDto;
        var getByIdFunc = _unitOfWork.PreferenceRepository.GetById;

        await _validationService.AssertNavigationEntityExists<Preference, Artist>(_unitOfWork.ArtistRepository,
            preferenceDto.Id, cancellationToken);
        await _validationService.EnsureUniqueProperty(getByIdFunc, nameof(preferenceDto.Id), preferenceDto.Id,
            cancellationToken);

        var preferenceEntity = _mapper.Map<Preference>(preferenceDto);
        var preferenceDb = await _unitOfWork.PreferenceRepository.Add(preferenceEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Preference), preferenceDb.Id);
        return _mapper.Map<PreferenceDto>(preferenceDb);
    }
}
