using Streamphony.Application.Interfaces;

namespace Streamphony.Application.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}