namespace Streamphony.Application.App.BlobStorage.DTOs;

public class BlobDto
{
    public required Guid Id { get; set; }
    public required string ContainerName { get; set; }
    public required string StorageKey { get; set; }
    public required string Url { get; set; }
    public TimeSpan? Duration { get; set; }
    public required Guid OwnerId { get; set; }
}
