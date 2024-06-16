namespace Streamphony.Application.Abstractions.Services;

public interface IAudioDurationService
{
    TimeSpan GetDuration(string audioFilePath);
}
