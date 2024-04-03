using System;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class Comment : BaseEntity
    {
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Comment text must be between 1 and 500 characters.")]
        public required string Text { get; set; }

        public required DateTime CreatedDate { get; init; } = DateTime.Now;
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid SongId { get; set; }
        public Song? Song { get; set; }
    }
}