namespace Streamphony.Application.Abstractions.Configurations;

public interface IFileStorageSettings
{
    long MaxAudioFileSize { get; }
    long MaxImageFileSize { get; }
    List<string> AllowedAudioExtensions { get; }
    List<string> AllowedImageExtensions { get; }
}
