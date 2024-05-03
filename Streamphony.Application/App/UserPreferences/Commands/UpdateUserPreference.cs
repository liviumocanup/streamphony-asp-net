using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.UserPreferences.Responses;

namespace Streamphony.Application.App.UserPreferences.Commands;

public record UpdateUserPreference(UserPreferenceDto UserPreferenceDto) : IRequest<UserPreferenceDto>;

public class UpdateUserPreferenceHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateUserPreference, UserPreferenceDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<UserPreferenceDto> Handle(UpdateUserPreference request, CancellationToken cancellationToken)
    {
        var userPreferenceDto = request.UserPreferenceDto;

        var userPreference = await _unitOfWork.UserPreferenceRepository.GetById(userPreferenceDto.Id, cancellationToken) ??
                    throw new KeyNotFoundException($"UserPreference with ID {userPreferenceDto.Id} not found.");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            _mapper.Map(userPreferenceDto, userPreference);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return _mapper.Map<UserPreferenceDto>(userPreference);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
