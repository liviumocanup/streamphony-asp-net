namespace Streamphony.Domain.Models
{
    public class UserPreference : BaseEntity
    {
        public bool DarkMode { get; set; } = false;
        public string Language { get; set; } = "en";
        public User User { get; set; } = default!;
        // public ICollection<Genre> PreferredGenres { get; set; } = new HashSet<Genre>();    }
    }
}
