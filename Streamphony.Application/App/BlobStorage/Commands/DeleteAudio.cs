using MediatR;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;

namespace Streamphony.Application.App.BlobStorage.Commands;

public record DeleteAudio(Guid Id) : IRequest<bool>;

public class DeleteAudioHandler(IBlobStorageService blobStorageService) : IRequestHandler<DeleteAudio, bool>
{
    private readonly IBlobStorageService _blobStorage = blobStorageService;

    public async Task<bool> Handle(DeleteAudio request, CancellationToken cancellationToken)
    {
        var songId = request.Id;
        await _blobStorage.DeleteFileAsync(BlobContainer.Songs, songId.ToString());
        return true;
    }
}
