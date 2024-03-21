using System;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
            Date = DateTime.Now;
        }

        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Comment text must be between 1 and 500 characters.")]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedDate { get; init; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; }
    }
}