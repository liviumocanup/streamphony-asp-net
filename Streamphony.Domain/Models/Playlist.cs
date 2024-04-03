using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class Playlist : BaseEntity
    {
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Playlist title must be between 1 and 100 characters.")]
        public required string Title { get; set; }

        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }
        public bool IsPublic { get; set; } = true;
        public ICollection<Song> Songs { get; set; } = new HashSet<Song>();

        public TimeSpan CalculateTotalDuration()
        {
            return new TimeSpan(Songs.Sum(song => song.Duration.Ticks));
        }
    }
}
