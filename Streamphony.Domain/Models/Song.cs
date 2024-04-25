using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class Song : BaseEntity
    {
        public required string Title { get; set; }

        public TimeSpan Duration { get; set; }

        [Url(ErrorMessage = "The URL must be a valid URL.")]
        public required string Url { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = default!;

        public Guid? GenreId { get; set; }
        public Genre? Genre { get; set; } = default!;
        
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }

        // public ICollection<Contributor> Contributors { get; set; } = new HashSet<Contributor>();
    }
}