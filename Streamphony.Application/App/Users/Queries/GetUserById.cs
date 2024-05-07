using MediatR;
using Streamphony.Domain.Models;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Services;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Application.App.Users.Queries;

public record GetUserById(Guid Id) : IRequest<UserDetailsDto>;

public class GetUserByIdHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger, IValidationService validationService) : IRequestHandler<GetUserById, UserDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMappingProvider _mapper = mapper;
    private readonly ILoggingProvider _logger = logger;
    private readonly IValidationService _validationService = validationService;

    public async Task<UserDetailsDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _validationService.GetExistingEntityWithInclude<User>(
            _unitOfWork.UserRepository.GetByIdWithInclude,
            request.Id,
            LogAction.Get,
            cancellationToken,
            user => user.UploadedSongs,
            user => user.Preferences,
            user => user.OwnedAlbums
        );

        _logger.LogInformation("{LogAction} success for {EntityType} with Id '{EntityId}'.", LogAction.Get, nameof(User), user.Id);
        return _mapper.Map<UserDetailsDto>(user);
    }
}