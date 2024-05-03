namespace Streamphony.WebAPI.Common.Models;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string StatusPhrase { get; set; } = default!;
    public List<string> Errors { get; } = [];
    public DateTime Timestamp { get; set; }
}