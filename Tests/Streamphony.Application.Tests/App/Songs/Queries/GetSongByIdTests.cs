using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Songs.Queries;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Songs.Queries;

[TestFixture]
public class GetSongByIdTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private GetSongByIdHandler _handler;
    private Song _existingSong;
    private SongDto _songDto;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new GetSongByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _existingSong = new Song
        {
            Id = Guid.NewGuid(),
            Title = "Unique Song",
            Duration = TimeSpan.FromMinutes(3),
            Url = "http://example.com/unique_song.mp3",
            OwnerId = Guid.NewGuid()
        };

        _songDto = new SongDto
        {
            Id = _existingSong.Id,
            Title = _existingSong.Title,
            Duration = _existingSong.Duration,
            Url = _existingSong.Url,
            OwnerId = _existingSong.OwnerId
        };

        _unitOfWorkMock.Setup(u => u.SongRepository.GetById(_existingSong.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(_existingSong);
        _mapperMock.Setup(m => m.Map<SongDto>(_existingSong)).Returns(_songDto);
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.SongRepository, _existingSong.Id, It.IsAny<CancellationToken>(), LogAction.Get))
                              .ReturnsAsync(_existingSong);
    }

    [Test]
    public async Task Handle_ValidId_ReturnsSongDto()
    {
        // Arrange
        var getSongByIdQuery = new GetSongById(_existingSong.Id);

        // Act
        var result = await _handler.Handle(getSongByIdQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(_songDto.Id));
            Assert.That(result.Title, Is.EqualTo(_songDto.Title));
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), _existingSong.Id, LogAction.Get), Times.Once());
    }

    [Test]
    public void Handle_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var invalidSongId = Guid.NewGuid();
        var getSongByIdQuery = new GetSongById(invalidSongId);
        _unitOfWorkMock.Setup(u => u.SongRepository.GetById(invalidSongId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Song)null!);

        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.SongRepository, invalidSongId, It.IsAny<CancellationToken>(), LogAction.Get))
                              .ThrowsAsync(new NotFoundException("Song not found."));

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(getSongByIdQuery, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(Song), invalidSongId, LogAction.Get), Times.Never());
    }
}
