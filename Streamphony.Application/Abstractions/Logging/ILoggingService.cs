using System.Runtime.CompilerServices;

namespace Streamphony.Application.Abstractions.Logging
{
    public interface ILoggingService
    {
        Task LogAsync(string message, [CallerMemberName] string methodName = "");
        Task LogAsync(string message, Exception ex, [CallerMemberName] string methodName = "");
    }
}