using Streamphony.Domain.Models.Auth;

namespace Streamphony.Domain.Models;

public class Artist : BaseEntity
{
    public required string StageName { get; set; }
    public required DateOnly DateOfBirth { get; set; }

    public Guid ProfilePictureBlobId { get; set; }
    public BlobFile ProfilePictureBlob { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<Song> UploadedSongs { get; set; } = new HashSet<Song>();
    public ICollection<Album> OwnedAlbums { get; set; } = new HashSet<Album>();
    public ICollection<AlbumArtist> AlbumContributions { get; set; } = new HashSet<AlbumArtist>();
    public Preference Preference { get; set; } = default!;
}
