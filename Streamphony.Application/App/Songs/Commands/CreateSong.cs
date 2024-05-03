using AutoMapper;
using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Songs.Commands;

public record CreateSong(SongCreationDto SongCreationDto) : IRequest<SongDto>;

public class CreateSongHandler : IRequestHandler<CreateSong, SongDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateSongHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService loggingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task<SongDto> Handle(CreateSong request, CancellationToken cancellationToken)
    {
        var songDto = request.SongCreationDto;
        if (await _unitOfWork.UserRepository.GetById(songDto.OwnerId, cancellationToken) == null)
            throw new KeyNotFoundException($"User with ID {songDto.OwnerId} not found.");

        var songEntity = _mapper.Map<Song>(songDto);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var songDb = await _unitOfWork.SongRepository.Add(songEntity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Song id {songDb.Id} - success");

            return _mapper.Map<SongDto>(songDb);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            await _loggingService.LogAsync($"Creation failure: ", ex);
            throw;
        }
    }
}