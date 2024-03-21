using System;
using System.Collections.Generic;

namespace Streamphony.Domain.Models
{
    public class UserPreferences : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Genre> PreferredGenres { get; set; } = new List<Genre>();
        public bool DarkMode { get; set; }
        public string Language { get; set; }

        public void AddPreferredGenre(Genre genre)
        {
            if (!PreferredGenres.Contains(genre))
            {
                PreferredGenres.Add(genre);
            }
        }

        public void RemovePreferredGenre(Genre genre)
        {
            PreferredGenres.Remove(genre);
        }
    }
}
