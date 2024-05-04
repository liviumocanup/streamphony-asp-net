using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record UpdateSong(SongDto SongDto) : IRequest<SongDto>;

public class UpdateSongHandler : IRequestHandler<UpdateSong, SongDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;

    public UpdateSongHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
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

            _logger.LogInformation("Successfully updated {EntityType} with Id {EntityId}.", nameof(Song), song.Id);

            return _mapper.Map<SongDto>(song);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogError("Failed to update {EntityType}. Error: {Error}", nameof(Song), ex.ToString());
            throw;
        }
    }

    private static async Task<T> GetEntityById<T>(IRepository<T> repository, Guid entityId, CancellationToken cancellationToken) where T : BaseEntity
    {
        return await repository.GetById(entityId, cancellationToken) ??
                throw new KeyNotFoundException($"{typeof(T).Name} with ID {entityId} not found.");
    }
}
