namespace Streamphony.Domain.Models
{
    public class UserPreferences : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Genre> PreferredGenres { get; set; } = new HashSet<Genre>();
        public bool DarkMode { get; set; } = false;
        public string Language { get; set; } = "en";
    }
}
