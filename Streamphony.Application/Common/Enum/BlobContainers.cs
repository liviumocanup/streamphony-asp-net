namespace Streamphony.Application.Common.Enum;

public abstract class BlobContainer
{
    public static string Draft => "draft";
    public static string Songs => "songs";
    public static string Images => "images";
    public static string ProfilePictures => $"{Images}/profile-pictures";
    public static string SongCovers => $"{Images}/song-covers";
    public static string AlbumCovers => $"{Images}/album-covers";
}
