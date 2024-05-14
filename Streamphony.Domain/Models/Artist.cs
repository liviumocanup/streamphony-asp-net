namespace Streamphony.Domain.Models;
public class Artist : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly DateOfBirth { get; set; }

    public string? ProfilePictureUrl { get; set; }
    public ICollection<Song> UploadedSongs { get; set; } = new HashSet<Song>();
    public ICollection<Album> OwnedAlbums { get; set; } = new HashSet<Album>();
    public ICollection<AlbumArtist> AlbumContributions { get; set; } = new HashSet<AlbumArtist>();
    public Preference Preference { get; set; } = default!;
}