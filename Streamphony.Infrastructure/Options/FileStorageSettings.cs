using Streamphony.Application.Abstractions.Configurations;

namespace Streamphony.Infrastructure.Options;

public class FileStorageSettings : IFileStorageSettings
{
    public long MaxAudioFileSize { get; init; }
    public long MaxImageFileSize { get; init; }
    public List<string> AllowedAudioExtensions { get; init; } = [];
    public List<string> AllowedImageExtensions { get; init; } = [];
}
