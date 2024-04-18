using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
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

        var userPreference = await _unitOfWork.UserPreferenceRepository.GetById(userPreferenceDto.Id) ??
                    throw new KeyNotFoundException($"UserPreference with ID {userPreferenceDto.Id} not found.");

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _mapper.Map(userPreferenceDto, userPreference);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<UserPreferenceDto>(userPreference);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
