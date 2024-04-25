using Streamphony.Application.Abstractions.Logging;

namespace Streamphony.Infrastructure.Logging
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}