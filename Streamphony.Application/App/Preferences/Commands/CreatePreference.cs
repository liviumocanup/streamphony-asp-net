using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Preferences.Responses;

namespace Streamphony.Application.App.Preferences.Commands;

public record CreatePreference(PreferenceDto PreferenceDto) : IRequest<PreferenceDto>;

public class CreatePreferenceHandler : IRequestHandler<CreatePreference, PreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingService _logger;
    private readonly IValidationService _validationService;

    public CreatePreferenceHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingService logger, IValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validationService = validationService;
    }

    public async Task<PreferenceDto> Handle(CreatePreference request, CancellationToken cancellationToken)
    {
        var preferenceDto = request.PreferenceDto;
        var getByIdFunc = _unitOfWork.PreferenceRepository.GetById;

        await _validationService.AssertNavigationEntityExists<Preference, Artist>(_unitOfWork.ArtistRepository, preferenceDto.Id, cancellationToken);
        await _validationService.EnsureUniqueProperty(getByIdFunc, nameof(preferenceDto.Id), preferenceDto.Id, cancellationToken);

        var preferenceEntity = _mapper.Map<Preference>(preferenceDto);
        var preferenceDb = await _unitOfWork.PreferenceRepository.Add(preferenceEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogSuccess(nameof(Preference), preferenceDb.Id);
        return _mapper.Map<PreferenceDto>(preferenceDb);
    }
}