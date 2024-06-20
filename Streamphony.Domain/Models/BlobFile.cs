namespace Streamphony.Domain.Models;

public class BlobFile : BaseEntity
{
    public required string ContainerName { get; set; }
    public required string StorageKey { get; set; }
    public required string Extension { get; set; }
    public required string ContentType { get; set; }
    public required long Length { get; set; }
    public required string Url { get; set; }
    public TimeSpan? Duration { get; set; }
    public required Guid OwnerId { get; set; }
    public Guid? RelatedEntityId { get; set; }
}
