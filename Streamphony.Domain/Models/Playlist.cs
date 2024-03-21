using System;
using System.Collections.Generic;

namespace Streamphony.Domain.Models
{
    public class Playlist : BaseEntity
    {
        public string Title { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<Song> Songs { get; set; } = new List<Song>();

        public void AddSong(Song song)
        {
            Songs.Add(song);
        }

        public void RemoveSong(Song song)
        {
            Songs.Remove(song);
        }

        public void RenamePlaylist(string newName)
        {
            Title = newName;
        }

        public TimeSpan CalculateTotalDuration()
        {
            return new TimeSpan(Songs.Sum(song => song.Duration.Ticks));
        }
    }
}
