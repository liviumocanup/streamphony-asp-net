using System;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class Rating : BaseEntity
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Score must be between 1 and 5.")]
        public int Score { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; }
    }
}
