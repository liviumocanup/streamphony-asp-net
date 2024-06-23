using MediatR;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Albums.Queries;

public record GetAlbumById(Guid Id) : IRequest<AlbumDetailsDto>;

public class GetAlbumByIdHandler(
    IUnitOfWork unitOfWork,
    IMappingProvider mapper,
    ILoggingService logger,
    IValidationService validationService) : IRequestHandler<GetAlbumById, AlbumDetailsDto>
{
    private readonly ILoggingService _logger = logger;
    private readonly IMappingProvider _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidationService _validationService = validationService;

    public async Task<AlbumDetailsDto> Handle(GetAlbumById request, CancellationToken cancellationToken)
    {
        var album = await _unitOfWork.AlbumRepository.GetByIdWithBlobs(request.Id, cancellationToken);
        
        _logger.LogSuccess(nameof(Album), album.Id, LogAction.Get);
        return _mapper.Map<AlbumDetailsDto>(album);
    }
}
