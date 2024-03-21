using System;
using System.Collections.Generic;

namespace Streamphony.Domain.Models
{
    public class Album : BaseEntity
    {
        public Guid OwnerId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImageUrl { get; set; }

        public User Owner { get; set; }
        public ICollection<User> Artists { get; set; } = new List<User>();
        public ICollection<Song> Songs { get; set; } = new List<Song>();

        public void AddSong(Song song)
        {
            Songs.Add(song);
        }

        public void RemoveSong(Song song)
        {
            Songs.Remove(song);
        }

        public void RenameAlbum(string newName)
        {
            Title = newName;
        }

        public TimeSpan CalculateTotalDuration()
        {
            return new TimeSpan(Songs.Sum(song => song.Duration.Ticks));
        }
    }
}
