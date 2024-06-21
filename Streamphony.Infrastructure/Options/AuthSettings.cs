namespace Streamphony.Infrastructure.Options;

public class AuthSettings
{
    public string Authority { get; init; } = default!;
    public string Audience { get; init; } = default!;
}
