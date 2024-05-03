namespace Streamphony.Domain.Models;
public class User : BaseEntity
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string ArtistName { get; set; }
    public required DateOnly DateOfBirth { get; set; }

    public string? ProfilePictureUrl { get; set; }
    public ICollection<Song> UploadedSongs { get; set; } = new HashSet<Song>();
    public ICollection<Album> OwnedAlbums { get; set; } = new HashSet<Album>();
    public ICollection<AlbumArtist> AlbumContributions { get; set; } = new HashSet<AlbumArtist>();
    public UserPreference Preferences { get; set; } = default!;
}