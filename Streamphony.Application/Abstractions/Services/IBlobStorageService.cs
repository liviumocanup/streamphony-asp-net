namespace Streamphony.Application.Abstractions.Services;

public interface IBlobStorageService
{
    Task<string> UploadFileAsync(string containerName, string fileName, string contentType, Stream content);
    Task DeleteFileAsync(string containerName, string fileName);
    Task<string> MoveBlobAsync(string sourceContainer, string sourceFileName, string destinationContainer, string destinationFileName);
}
