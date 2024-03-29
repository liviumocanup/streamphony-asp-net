using System.Runtime.CompilerServices;

namespace Streamphony.Application.Interfaces
{
    public interface ILoggingService
    {
        Task LogAsync(string message, [CallerMemberName] string methodName = "");
    }
}