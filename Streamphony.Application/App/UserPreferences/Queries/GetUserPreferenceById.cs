using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.UserPreferences.Responses;

namespace Streamphony.Application.App.UserPreferences.Queries;

public record GetUserPreferenceById(Guid Id) : IRequest<UserPreferenceDto>;

public class GetUserPreferenceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserPreferenceById, UserPreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<UserPreferenceDto> Handle(GetUserPreferenceById request, CancellationToken cancellationToken)
    {
        var userPreference = await _unitOfWork.UserPreferenceRepository.GetById(request.Id, cancellationToken);

        return _mapper.Map<UserPreferenceDto>(userPreference);
    }
}