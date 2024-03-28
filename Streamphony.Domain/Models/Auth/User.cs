// using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class User : BaseEntity
    {
        private DateTime _dateOfBirth;

        [Required(ErrorMessage = "Artist name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Artist name must be between 1 and 100 characters.")]
        public string ArtistName { get; set; }

        [Required]
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.UtcNow)
                {
                    throw new ArgumentException("DateOfBirth cannot be in the future.");
                }
                _dateOfBirth = value;
            }
        }

        [Url(ErrorMessage = "The Profile picture URL must be a valid URL.")]
        public string ProfilePictureUrl { get; set; }

        public ICollection<Song> UploadedSongs { get; set; } = new HashSet<Song>();
        // public ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}
