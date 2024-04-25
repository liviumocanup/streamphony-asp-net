namespace Streamphony.Application.Abstractions.Logging
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}