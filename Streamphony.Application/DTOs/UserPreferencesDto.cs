namespace Streamphony.Application.DTOs
{
    public class UserPreferencesDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<GenreDto> PreferredGenres { get; set; }
        public bool DarkMode { get; set; }
        public string Language { get; set; }
    }
}