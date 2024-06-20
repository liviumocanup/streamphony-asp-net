using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Infrastructure.ServiceProviders.FileStorage;

public class AzuriteBlobStorageService(string? connectionString) : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
    private static readonly PublicAccessType PublicAccessType = PublicAccessType.Blob;
    
    public async Task<string> UploadFileAsync(string containerName, string fileName, string contentType, Stream content)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(publicAccessType: PublicAccessType);
        
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
    
    public async Task<string> MoveBlobAsync(string sourceContainer, string sourceFileName, string destinationContainer, string destinationFileName)
    {
        var sourceContainerClient = _blobServiceClient.GetBlobContainerClient(sourceContainer);
        var destinationContainerClient = _blobServiceClient.GetBlobContainerClient(destinationContainer);

        await destinationContainerClient.CreateIfNotExistsAsync(publicAccessType: PublicAccessType);

        var sourceBlobClient = sourceContainerClient.GetBlobClient(sourceFileName);
        var destinationBlobClient = destinationContainerClient.GetBlobClient(destinationFileName);

        if (!await sourceBlobClient.ExistsAsync()) 
            throw new FileNotFoundException("Source blob does not exist.");
        
        await destinationBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri);
        await sourceBlobClient.DeleteAsync();

        return destinationBlobClient.Uri.AbsoluteUri;
    }

}
