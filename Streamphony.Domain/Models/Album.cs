using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class Album : BaseEntity
    {
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Album title must be between 1 and 100 characters.")]
        public required string Title { get; set; }

        [Url(ErrorMessage = "The Cover image URL must be a valid URL.")]
        public string? CoverImageUrl { get; set; }

        public DateTime ReleaseDate { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = default!;
        public ICollection<User> Artists { get; set; } = new HashSet<User>();
        public ICollection<Song> Songs { get; set; } = new HashSet<Song>();

        public TimeSpan CalculateTotalDuration()
        {
            return new TimeSpan(Songs.Sum(song => song.Duration.Ticks));
        }
    }
}
