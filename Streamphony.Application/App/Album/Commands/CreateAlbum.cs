using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Commands;

public record CreateAlbum(AlbumCreationDto AlbumCreationDto) : IRequest<AlbumDto>;

public class CreateAlbumHandler : IRequestHandler<CreateAlbum, AlbumDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateAlbumHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService loggingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<AlbumDto> Handle(CreateAlbum request, CancellationToken cancellationToken)
    {
        var albumDto = request.AlbumCreationDto;
        if (await _unitOfWork.UserRepository.GetById(albumDto.OwnerId, cancellationToken) == null)
            throw new KeyNotFoundException($"User with ID {albumDto.OwnerId} not found.");

        var albumEntity = _mapper.Map<Album>(albumDto);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var albumDb = await _unitOfWork.AlbumRepository.Add(albumEntity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Album id {albumDb.Id} - success");

            return _mapper.Map<AlbumDto>(albumDb);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Creation failure: ", ex);
            throw;
        }
    }
}