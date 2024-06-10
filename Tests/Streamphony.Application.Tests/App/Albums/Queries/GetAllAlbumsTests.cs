using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Queries;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Albums.Queries;

[TestFixture]
public class GetAllAlbumsTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _handler = new GetAllAlbumsHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);

        _albums =
        [
            new Album { Id = Guid.NewGuid(), Title = "First Album" },
            new Album { Id = Guid.NewGuid(), Title = "Second Album" }
        ];

        _albumDtos = _albums.Select(album => new AlbumDto { Id = album.Id, Title = album.Title }).ToList();

        _unitOfWorkMock.Setup(u => u.AlbumRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(_albums);
        _mapperMock.Setup(m => m.Map<IEnumerable<AlbumDto>>(_albums)).Returns(_albumDtos);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private GetAllAlbumsHandler _handler;
    private List<Album> _albums;
    private List<AlbumDto> _albumDtos;

    [Test]
    public async Task Handle_AlbumsExist_ReturnsAllAlbumDtos()
    {
        // Arrange
        var getAllAlbumsQuery = new GetAllAlbums();

        // Act
        var result = await _handler.Handle(getAllAlbumsQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(_albumDtos.Count));
            Assert.That(result, Is.EquivalentTo(_albumDtos));
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(Album), LogAction.Get), Times.Once());
    }

    [Test]
    public async Task Handle_NoAlbumsExist_ReturnsEmpty()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.AlbumRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        var getAllAlbumsQuery = new GetAllAlbums();

        // Act
        var result = await _handler.Handle(getAllAlbumsQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(Album), LogAction.Get), Times.Once());
    }
}
