using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.UserPreferences.Responses;

namespace Streamphony.Application.App.UserPreferences.Queries;

public class GetAllUserPreferences() : IRequest<IEnumerable<UserPreferenceDto>>;

public class GetAllUserPreferencesHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUserPreferences, IEnumerable<UserPreferenceDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UserPreferenceDto>> Handle(GetAllUserPreferences request, CancellationToken cancellationToken)
    {
        var userPreferences = await _unitOfWork.UserPreferenceRepository.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<UserPreferenceDto>>(userPreferences);
    }
}