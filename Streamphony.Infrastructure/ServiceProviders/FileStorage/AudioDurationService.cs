using NAudio.Wave;
using Streamphony.Application.Abstractions.Services;

namespace Streamphony.Infrastructure.ServiceProviders.FileStorage;

public class AudioDurationService : IAudioDurationService
{
    public TimeSpan GetDuration(string filePath)
    {
        using var reader = new AudioFileReader(filePath);
        return reader.TotalTime;
    }
}
