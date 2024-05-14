namespace Streamphony.Application.App.Preferences.Responses;

public class PreferenceDto
{
    public Guid Id { get; set; }
    public bool DarkMode { get; set; }
    public string? Language { get; set; }
}
