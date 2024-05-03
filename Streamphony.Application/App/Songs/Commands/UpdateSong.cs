using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record UpdateSong(SongDto SongDto) : IRequest<SongDto>;

public class UpdateSongHandler : IRequestHandler<UpdateSong, SongDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public UpdateSongHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService loggingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<SongDto> Handle(UpdateSong request, CancellationToken cancellationToken)
    {
        var songDto = request.SongDto;
        var song = await GetEntityById(_unitOfWork.SongRepository, songDto.Id, cancellationToken);
        var user = await GetEntityById(_unitOfWork.UserRepository, songDto.OwnerId, cancellationToken);

        if (songDto.GenreId != null) await GetEntityById(_unitOfWork.GenreRepository, songDto.GenreId.Value, cancellationToken);
        if (songDto.AlbumId != null) await GetEntityById(_unitOfWork.AlbumRepository, songDto.AlbumId.Value, cancellationToken);
        if (!user.UploadedSongs.Any(a => a.Id == songDto.Id))
            throw new KeyNotFoundException($"User with ID {songDto.OwnerId} does not own song with ID {songDto.Id}");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            _mapper.Map(songDto, song);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Song id {song.Id} - updated");

            return _mapper.Map<SongDto>(song);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Error updating song id {songDto.Id}: ", ex);
            throw;
        }
    }

    private static async Task<T> GetEntityById<T>(IRepository<T> repository, Guid entityId, CancellationToken cancellationToken) where T : BaseEntity
    {
        return await repository.GetById(entityId, cancellationToken) ??
                throw new KeyNotFoundException($"{typeof(T).Name} with ID {entityId} not found.");
    }
}