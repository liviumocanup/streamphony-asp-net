namespace Streamphony.Domain.Models;

public class Preference : BaseEntity
{
    public bool DarkMode { get; set; } = false;
    public string Language { get; set; } = "en";
    public Artist Artist { get; set; } = default!;
}
