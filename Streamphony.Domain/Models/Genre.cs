using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Genre name must be between 1 and 50 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description must be less than 100 characters.")]
        public string Description { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
