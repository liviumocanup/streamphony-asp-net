namespace Streamphony.Domain.Models
{
    public class User : BaseEntity
    {
        private DateOnly _dateOfBirth;
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string ArtistName { get; set; }

        public required DateOnly DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new ArgumentException("DateOfBirth cannot be in the future.");
                }
                _dateOfBirth = value;
            }
        }

        public string? ProfilePictureUrl { get; set; }
        public ICollection<Song> UploadedSongs { get; set; } = new HashSet<Song>();
        public ICollection<Album> OwnedAlbums { get; set; } = new HashSet<Album>();
        public ICollection<AlbumArtist> AlbumContributions { get; set; } = new HashSet<AlbumArtist>();

        public UserPreference Preferences { get; set; } = default!;
    }
}
