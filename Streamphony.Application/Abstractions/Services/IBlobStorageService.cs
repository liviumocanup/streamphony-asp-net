namespace Streamphony.Application.Abstractions.Services;

public interface IBlobStorageService
{
    Task<string> UploadAudioFileAsync(string fileName, Stream content);
    Task<string> UploadImageFileAsync(string containerName, string fileName, Stream content);
    Task DeleteFileAsync(string containerName, string fileName);
}
