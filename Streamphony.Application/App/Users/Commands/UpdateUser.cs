using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.App.Users.Responses;

namespace Streamphony.Application.App.Users.Commands;

public record UpdateUser(UserDto UserDto) : IRequest<UserDto>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService loggingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<UserDto> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var userDto = request.UserDto;
        var user = await _unitOfWork.UserRepository.GetById(userDto.Id) ??
                    throw new KeyNotFoundException($"User with ID {userDto.Id} not found.");

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _mapper.Map(userDto, user);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            await _loggingService.LogAsync($"User id {user.Id} - updated");

            return _mapper.Map<UserDto>(user);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();

            await _loggingService.LogAsync($"Error updating user id {userDto.Id}: ", ex);
            throw;
        }
    }
}
