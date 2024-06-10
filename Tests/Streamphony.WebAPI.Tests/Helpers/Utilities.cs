using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.WebAPI.Tests.Helpers;

public static class Utilities
{
    public static Guid UserId1 { get; set; } = Guid.Parse("6F9619FF-8B86-D011-B42D-00C04FC964FF");
    public static Guid UserId2 { get; set; } = Guid.Parse("28FE86CE-2EE3-4115-9E87-8E07BA7DEC48");
    public static Guid AlbumId1 { get; set; } = Guid.Parse("2ADC4E8E-D274-46AF-F488-08DC709A07E3");
    public static Guid AlbumId2 { get; set; } = Guid.Parse("9FDE9A46-4BC8-43EB-A8CF-26EF46E2F406");
    public static Guid GenreId1 { get; set; } = Guid.Parse("E50D5E93-2043-4EF8-A231-08DC5EC291B2");
    public static Guid GenreId2 { get; set; } = Guid.Parse("1D6A5466-D424-441A-27C4-08DC5EC291A0");
    public static Guid SongId1 { get; set; } = Guid.Parse("D1D3D3D3-3D3D-3D3D-3D3D-3D3D3D3D3D3D");
    public static Guid SongId2 { get; set; } = Guid.Parse("D2D3D3D3-3D3D-3D3D-3D3D-3D3D3D3D3D3D");

    public static User DbUser1 { get; set; } = new()
    {
        Id = UserId1,
        Username = "TestUser",
        ArtistName = "TestArtist",
        Email = "test@mail.com",
        DateOfBirth = new DateOnly(1990, 1, 1)
    };

    public static User DbUser2 { get; set; } = new()
    {
        Id = UserId2,
        Username = "UpdatesUser",
        ArtistName = "UpdatedArtist",
        Email = "updated@mail.com",
        DateOfBirth = new DateOnly(1990, 1, 1)
    };

    public static Album DbAlbum1 { get; set; } = new() { Id = AlbumId1, Title = "TestAlbum", OwnerId = UserId1 };
    public static Album DbAlbum2 { get; set; } = new() { Id = AlbumId2, Title = "UpdatedAlbum", OwnerId = UserId1 };

    public static Genre DbGenre1 { get; set; } =
        new() { Id = GenreId1, Name = "TestGenre", Description = "TestDescription" };

    public static Genre DbGenre2 { get; set; } =
        new() { Id = GenreId2, Name = "UpdatedGenre", Description = "TestDescription" };

    public static Song DbSong1 { get; set; } = new()
    {
        Id = SongId1,
        Title = "TestSong",
        Duration = new TimeSpan(0, 2, 5),
        OwnerId = UserId1,
        AlbumId = AlbumId1,
        GenreId = GenreId1,
        Url = "https://test.com"
    };

    public static Song DbSong2 { get; set; } = new()
    {
        Id = SongId2,
        Title = "UpdatedSong",
        Duration = new TimeSpan(0, 1, 32),
        OwnerId = UserId1,
        AlbumId = AlbumId2,
        GenreId = GenreId2,
        Url = "https://test.com"
    };

    public static UserPreference DbUserPreference { get; set; } = new() { Id = UserId2, Language = "en" };

    public static void InitializeDbForTests(ApplicationDbContext db)
    {
        db.Users.AddRange(DbUser1, DbUser2);
        db.Albums.AddRange(DbAlbum1, DbAlbum2);
        db.Genres.AddRange(DbGenre1, DbGenre2);
        db.Songs.AddRange(DbSong1, DbSong2);
        db.UserPreferences.Add(DbUserPreference);
        db.SaveChanges();
    }
}
