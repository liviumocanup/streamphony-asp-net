using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.App.Auth.Commands;

public record RefreshToken(Guid UserId) : IRequest<AuthResultDto>;

public class RefreshTokenHandler(
    ILoggingService loggingService,
    IUserManagerProvider userManagerProvider,
    IAuthenticationService authenticationService,
    IValidationService validationService,
    IUnitOfWork unitOfWork
) : IRequestHandler<RefreshToken, AuthResultDto>
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly ILoggingService _loggingService = loggingService;
    private readonly IUserManagerProvider _userManagerProvider = userManagerProvider;
    private readonly IValidationService _validationService = validationService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthResultDto> Handle(RefreshToken request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        
        var userDb = await _validationService.GetExistingEntity(_userManagerProvider, userId, cancellationToken);
        
        var artistDb = await _unitOfWork.ArtistRepository.FindByUserIdAsync(userId, cancellationToken);

        userDb.Artist = artistDb;

        var token = await _authenticationService.RefreshToken(userDb.Id);
        if (token == null) _loggingService.LogAndThrowNotAuthorizedException(nameof(User));

        return new AuthResultDto(token!);
    }
}
