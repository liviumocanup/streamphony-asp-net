namespace Streamphony.Application.App.BlobStorage.DTOs;

public class BlobRequestDto
{
    public required string FileName { get; set; }
    public required long Length { get; set; }
    public required string ContentType { get; set; }
    public required Stream? Content { get; set; }
}
