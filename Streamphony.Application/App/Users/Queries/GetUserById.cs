using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.App.Users.Responses;

namespace Streamphony.Application.App.Users.Queries;

public record GetUserById(Guid Id) : IRequest<UserDetailsDto>;

public class GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserById, UserDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDetailsDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdWithInclude(request.Id, user => user.UploadedSongs, user => user.Preferences, user => user.OwnedAlbums);

        if (user == null) return null!;

        return _mapper.Map<UserDetailsDto>(user);
    }
}