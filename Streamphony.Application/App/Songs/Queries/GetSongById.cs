using AutoMapper;
using MediatR;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Queries;

public record GetSongById(Guid Id) : IRequest<SongDto>;

public class GetSongByIdHandler(IRepository<Song> repository, IMapper mapper) : IRequestHandler<GetSongById, SongDto>
{
    private readonly IRepository<Song> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<SongDto> Handle(GetSongById request, CancellationToken cancellationToken)
    {
        var song = await _repository.GetById(request.Id);

        return _mapper.Map<SongDto>(song);
    }
}