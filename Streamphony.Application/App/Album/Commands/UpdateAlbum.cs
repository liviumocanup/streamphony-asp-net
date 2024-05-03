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
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public UpdateAlbumHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService loggingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggingService = loggingService;
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

            await _loggingService.LogAsync($"Album id {album.Id} - updated");

            return _mapper.Map<AlbumDto>(album);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Error updating album id {albumDto.Id}: ", ex);
            throw;
        }
    }

    private static async Task<T> GetEntityById<T>(IRepository<T> repository, Guid entityId, CancellationToken cancellationToken) where T : BaseEntity
    {
        return await repository.GetById(entityId, cancellationToken) ??
                throw new KeyNotFoundException($"{typeof(T).Name} with ID {entityId} not found.");
    }
}
