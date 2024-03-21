using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class Song : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Song title must be between 1 and 50 characters.")]
        public string Title { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Url(ErrorMessage = "The URL must be a valid URL.")]
        public string Url { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<Contributor> Contributors { get; set; } = new HashSet<Contributor>();
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
        public Album Album { get; set; }
    }
}
