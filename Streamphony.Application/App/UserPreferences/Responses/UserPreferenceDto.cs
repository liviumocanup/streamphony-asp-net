namespace Streamphony.Application.App.UserPreferences.Responses;

public class UserPreferenceDto
{
    public Guid Id { get; set; }
    public bool DarkMode { get; set; }
    public string? Language { get; set; }
    // public ICollection<GenreDto?> PreferredGenres { get; set; }
}
