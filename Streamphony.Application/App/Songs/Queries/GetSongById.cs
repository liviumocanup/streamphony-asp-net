using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.App.Songs.Queries;

public record GetSongById(Guid Id) : IRequest<SongDto>;

public class GetSongByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetSongById, SongDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<SongDto> Handle(GetSongById request, CancellationToken cancellationToken)
    {
        var song = await _unitOfWork.SongRepository.GetById(request.Id, cancellationToken);

        return _mapper.Map<SongDto>(song);
    }
}