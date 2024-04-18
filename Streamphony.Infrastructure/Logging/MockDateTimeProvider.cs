using Streamphony.Application.Abstractions.Logging;

namespace Streamphony.Infrastructure.Logging
{
    public class MockDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => new(2023, 1, 1);
    }
}