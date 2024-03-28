using Streamphony.Application.Interfaces;

namespace Streamphony.Application.Services
{
    public class MockDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => new DateTime(2023, 1, 1);
    }
}