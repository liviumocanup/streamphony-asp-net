using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.WebAPI.Tests.Helpers;

public static class Utilities
{
    private static Guid userId1 = Guid.Parse("6F9619FF-8B86-D011-B42D-00C04FC964FF");
    private static Guid userId2 = Guid.Parse("28FE86CE-2EE3-4115-9E87-8E07BA7DEC48");
    private static Guid albumId1 = Guid.Parse("2ADC4E8E-D274-46AF-F488-08DC709A07E3");
    private static Guid albumId2 = Guid.Parse("9FDE9A46-4BC8-43EB-A8CF-26EF46E2F406");
    private static Guid genreId1 = Guid.Parse("E50D5E93-2043-4EF8-A231-08DC5EC291B2");
    private static Guid genreId2 = Guid.Parse("1D6A5466-D424-441A-27C4-08DC5EC291A0");
    private static Guid songId1 = Guid.Parse("D1D3D3D3-3D3D-3D3D-3D3D-3D3D3D3D3D3D");
    private static Guid songId2 = Guid.Parse("D2D3D3D3-3D3D-3D3D-3D3D-3D3D3D3D3D3D");
    private static User dbUser1 = new()
    {
        Id = UserId1,
        Username = "TestUser",
        ArtistName = "TestArtist",
        Email = "test@mail.com",
        DateOfBirth = new DateOnly(1990, 1, 1)
    };
    private static User dbUser2 = new()
    {
        Id = UserId2,
        Username = "UpdatesUser",
        ArtistName = "UpdatedArtist",
        Email = "updated@mail.com",
        DateOfBirth = new DateOnly(1990, 1, 1)
    };
    private static Album dbAlbum1 = new() { Id = AlbumId1, Title = "TestAlbum", OwnerId = UserId1 };
    private static Album dbAlbum2 = new() { Id = AlbumId2, Title = "UpdatedAlbum", OwnerId = UserId1 };
    private static Genre dbGenre1 = new() { Id = GenreId1, Name = "TestGenre", Description = "TestDescription" };
    private static Genre dbGenre2 = new() { Id = GenreId2, Name = "UpdatedGenre", Description = "TestDescription" };
    private static Song dbSong1 = new()
    {
        Id = SongId1,
        Title = "TestSong",
        Duration = new TimeSpan(0, 2, 5),
        OwnerId = UserId1,
        AlbumId = AlbumId1,
        GenreId = GenreId1,
        Url = "https://test.com"
    };
    private static Song dbSong2 = new()
    {
        Id = SongId2,
        Title = "UpdatedSong",
        Duration = new TimeSpan(0, 1, 32),
        OwnerId = UserId1,
        AlbumId = AlbumId2,
        GenreId = GenreId2,
        Url = "https://test.com"
    };
    private static UserPreference dbUserPreference = new() { Id = userId2, Language = "en" };
    public static void InitializeDbForTests(ApplicationDbContext db)
    {
        db.Users.AddRange(DbUser1, DbUser2);
        db.Albums.AddRange(DbAlbum1, DbAlbum2);
        db.Genres.AddRange(DbGenre1, DbGenre2);
        db.Songs.AddRange(DbSong1, DbSong2);
        db.UserPreferences.Add(DbUserPreference);
        db.SaveChanges();
    }

    public static Guid UserId1 { get => userId1; set => userId1 = value; }
    public static Guid UserId2 { get => userId2; set => userId2 = value; }
    public static Guid AlbumId1 { get => albumId1; set => albumId1 = value; }
    public static Guid AlbumId2 { get => albumId2; set => albumId2 = value; }
    public static Guid GenreId1 { get => genreId1; set => genreId1 = value; }
    public static Guid GenreId2 { get => genreId2; set => genreId2 = value; }
    public static Guid SongId1 { get => songId1; set => songId1 = value; }
    public static Guid SongId2 { get => songId2; set => songId2 = value; }
    public static User DbUser1 { get => dbUser1; set => dbUser1 = value; }
    public static User DbUser2 { get => dbUser2; set => dbUser2 = value; }
    public static Album DbAlbum1 { get => dbAlbum1; set => dbAlbum1 = value; }
    public static Album DbAlbum2 { get => dbAlbum2; set => dbAlbum2 = value; }
    public static Genre DbGenre1 { get => dbGenre1; set => dbGenre1 = value; }
    public static Genre DbGenre2 { get => dbGenre2; set => dbGenre2 = value; }
    public static Song DbSong1 { get => dbSong1; set => dbSong1 = value; }
    public static Song DbSong2 { get => dbSong2; set => dbSong2 = value; }
    public static UserPreference DbUserPreference { get => dbUserPreference; set => dbUserPreference = value; }
}