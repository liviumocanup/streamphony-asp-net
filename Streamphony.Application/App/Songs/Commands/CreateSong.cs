using AutoMapper;
using MediatR;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record CreateSong(SongDto SongDto) : IRequest<SongDto>;

public class CreateSongHandler : IRequestHandler<CreateSong, SongDto>
{
    private readonly IRepository<Song> _songRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateSongHandler(IRepository<Song> songRepository, IRepository<User> userRepository, IMapper mapper, ILoggingService loggingService)
    {
        _songRepository = songRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<SongDto> Handle(CreateSong request, CancellationToken cancellationToken)
    {
        var ownerId = request.SongDto.OwnerId;

        try
        {
            var user = await _userRepository.GetById(ownerId) ?? throw new KeyNotFoundException($"User with ID {ownerId} not found.");
            var songEntity = _mapper.Map<Song>(request.SongDto);

            _songRepository.Add(songEntity);
            await _songRepository.SaveChangesAsync();
            
            UpdateUserSongList(user, songEntity.Id);
            await _loggingService.LogAsync($"Song id {songEntity.Id} - success");

            return _mapper.Map<SongDto>(songEntity);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            await _loggingService.LogAsync($"Creation failure: {errorMessage}");
            throw;
        }
    }

    private async void UpdateUserSongList(User user, Guid songId)
    {
        var songRepo = await _songRepository.GetById(songId);
        user.UploadedSongs.Add(songRepo);
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }
}