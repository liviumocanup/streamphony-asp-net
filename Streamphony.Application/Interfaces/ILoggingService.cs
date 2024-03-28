namespace Streamphony.Application.Interfaces
{
    public interface ILoggingService
    {
        Task LogAsync(string message);
    }
}
