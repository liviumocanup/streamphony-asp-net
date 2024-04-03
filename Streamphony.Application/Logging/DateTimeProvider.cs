using Streamphony.Application.Interfaces;

namespace Streamphony.Application.Logging
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}