using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.Queries;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Songs.Queries;

[TestFixture]
public class GetAllSongsTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private GetAllSongsHandler _handler;
    private List<Song> _songs;
    private List<SongDto> _songDtos;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _handler = new GetAllSongsHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);

        _songs =
        [
            new() { Id = Guid.NewGuid(), Title = "Song One", Duration = TimeSpan.FromMinutes(5), Url = "http://example.com/song1.mp3", OwnerId = Guid.NewGuid() },
            new() { Id = Guid.NewGuid(), Title = "Song Two", Duration = TimeSpan.FromMinutes(3), Url = "http://example.com/song2.mp3", OwnerId = Guid.NewGuid() }
        ];

        _songDtos = _songs.Select(song => new SongDto
        {
            Id = song.Id,
            Title = song.Title,
            Duration = song.Duration,
            Url = song.Url,
            OwnerId = song.OwnerId
        }).ToList();

        _unitOfWorkMock.Setup(u => u.SongRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(_songs);
        _mapperMock.Setup(m => m.Map<IEnumerable<SongDto>>(_songs)).Returns(_songDtos);
    }

    [Test]
    public async Task Handle_SongsExist_ReturnsAllSongDtos()
    {
        // Arrange
        var getAllSongsQuery = new GetAllSongs();

        // Act
        var result = await _handler.Handle(getAllSongsQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(_songDtos.Count));
            Assert.That(result, Is.EquivalentTo(_songDtos));
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), LogAction.Get), Times.Once());
    }

    [Test]
    public async Task Handle_NoSongsExist_ReturnsEmpty()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.SongRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Song>());
        var getAllSongsQuery = new GetAllSongs();

        // Act
        var result = await _handler.Handle(getAllSongsQuery, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), LogAction.Get), Times.Once());
    }
}
