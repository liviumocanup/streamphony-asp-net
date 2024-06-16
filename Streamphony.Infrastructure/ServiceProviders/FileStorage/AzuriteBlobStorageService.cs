using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.Common.Enum;

namespace Streamphony.Infrastructure.ServiceProviders.FileStorage;

public class AzuriteBlobStorageService(string? connectionString) : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
    
    public async Task<string> UploadAudioFileAsync(string fileName, Stream content)
    {
        return await UploadFileAsync(BlobContainer.Songs, fileName, content, "audio/mpeg");
    }

    public async Task<string> UploadImageFileAsync(string containerName, string fileName, Stream content)
    {
        return await UploadFileAsync(containerName, fileName, content, "image/png");
    }
    
    private async Task<string> UploadFileAsync(string containerName, string fileName, Stream content, string contentType)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        
        var blobClient = containerClient.GetBlobClient(fileName);
        var blobHttpHeader = new BlobHttpHeaders { ContentType = contentType };
        await blobClient.UploadAsync(content, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
        
        return blobClient.Uri.AbsoluteUri;
    }

    public async Task DeleteFileAsync(string containerName, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        
        var blobClient = containerClient.GetBlobClient(fileName);
        
        await blobClient.DeleteIfExistsAsync();
    }
}
