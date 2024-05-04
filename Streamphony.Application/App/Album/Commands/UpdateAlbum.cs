using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Commands;

public record UpdateAlbum(AlbumDto AlbumDto) : IRequest<AlbumDto>;

public class UpdateAlbumHandler : IRequestHandler<UpdateAlbum, AlbumDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingProvider _mapper;
    private readonly ILoggingProvider _logger;

    public UpdateAlbumHandler(IUnitOfWork unitOfWork, IMappingProvider mapper, ILoggingProvider logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AlbumDto> Handle(UpdateAlbum request, CancellationToken cancellationToken)
    {
        var albumDto = request.AlbumDto;
        var album = await GetEntityById(_unitOfWork.AlbumRepository, albumDto.Id, cancellationToken);
        var user = await GetEntityById(_unitOfWork.UserRepository, albumDto.OwnerId, cancellationToken);

        if (!user.OwnedAlbums.Any(a => a.Id == albumDto.Id))
            throw new KeyNotFoundException($"User with ID {albumDto.OwnerId} does not own album with ID {albumDto.Id}");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            _mapper.Map(albumDto, album);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            _logger.LogInformation("Successfully updated {EntityType} with Id {EntityId}.", nameof(Album), album.Id);

            return _mapper.Map<AlbumDto>(album);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogError("Failed to update {EntityType}. Error: {Error}", nameof(Album), ex.ToString());
            throw;
        }
    }

    private static async Task<T> GetEntityById<T>(IRepository<T> repository, Guid entityId, CancellationToken cancellationToken) where T : BaseEntity
    {
        return await repository.GetById(entityId, cancellationToken) ??
                throw new KeyNotFoundException($"{typeof(T).Name} with ID {entityId} not found.");
    }
}
