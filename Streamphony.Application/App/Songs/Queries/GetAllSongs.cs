using MediatR;
using Streamphony.Application.Interfaces.Repositories;
using AutoMapper;
using Streamphony.Domain.Models;
using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.Application.App.Songs.Queries;

public class GetAllSongs() : IRequest<IEnumerable<SongDto>>;

public class GetAllSongsHandler(IRepository<Song> repository, IMapper mapper) : IRequestHandler<GetAllSongs, IEnumerable<SongDto>>
{
    private readonly IRepository<Song> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<SongDto>> Handle(GetAllSongs request, CancellationToken cancellationToken)
    {
        var songs = await _repository.GetAll();

        return _mapper.Map<IEnumerable<SongDto>>(songs);
    }
}