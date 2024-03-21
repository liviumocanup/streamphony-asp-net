using System;

namespace Streamphony.Domain.Models
{
    public class Song : BaseEntity
    {
        public Guid OwnerId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
        public string Url { get; set; }

        public User Owner { get; set; }
        public Album Album { get; set; }
    }
}
