using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.App.UserPreferences.Responses;

namespace Streamphony.Application.App.Users.Responses;

public class UserDetailsDto : UserDto
{
    public ICollection<SongDto?> UploadedSongs { get; set; } = new HashSet<SongDto?>();
    public ICollection<AlbumDto?> OwnedAlbums { get; set; } = new HashSet<AlbumDto?>();
    public UserPreferenceDto? Preferences { get; set; }
}