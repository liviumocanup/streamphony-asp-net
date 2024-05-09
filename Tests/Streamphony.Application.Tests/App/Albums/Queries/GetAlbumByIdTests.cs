using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Queries;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Albums.Queries;

[TestFixture]
public class GetAlbumByIdTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private GetAlbumByIdHandler _handler;
    private Album _existingAlbum;
    private AlbumDetailsDto _albumDetailsDto;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new GetAlbumByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _existingAlbum = new Album
        {
            Id = Guid.NewGuid(),
            Title = "Sample Album",
        };

        _albumDetailsDto = new AlbumDetailsDto
        {
            Id = _existingAlbum.Id,
            Title = _existingAlbum.Title
        };
    }

    [Test]
    public async Task Handle_ValidId_ReturnsAlbumDetailsDto()
    {
        // Arrange
        var albumId = _existingAlbum.Id;
        var getAlbumByIdQuery = new GetAlbumById(albumId);

        _unitOfWorkMock.Setup(u => u.AlbumRepository.GetByIdWithInclude(albumId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_existingAlbum);

        _validationServiceMock.Setup(v => v.GetExistingEntityWithInclude<Album>(
            _unitOfWorkMock.Object.AlbumRepository.GetByIdWithInclude,
            albumId,
            LogAction.Get,
            It.IsAny<CancellationToken>(),
            album => album.Songs))
            .ReturnsAsync(_existingAlbum);

        _mapperMock.Setup(m => m.Map<AlbumDetailsDto>(_existingAlbum)).Returns(_albumDetailsDto);

        // Act
        var result = await _handler.Handle(getAlbumByIdQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(_albumDetailsDto.Id));
            Assert.That(result.Title, Is.EqualTo(_albumDetailsDto.Title));
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(Album), albumId, LogAction.Get), Times.Once());
    }

    [Test]
    public void Handle_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var invalidAlbumId = Guid.NewGuid();
        var getAlbumByIdQuery = new GetAlbumById(invalidAlbumId);

        _unitOfWorkMock.Setup(u => u.AlbumRepository.GetByIdWithInclude(invalidAlbumId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Album)null!);

        _validationServiceMock.Setup(v => v.GetExistingEntityWithInclude<Album>(
            _unitOfWorkMock.Object.AlbumRepository.GetByIdWithInclude,
            invalidAlbumId,
            LogAction.Get,
            It.IsAny<CancellationToken>(),
            album => album.Songs))
            .ThrowsAsync(new NotFoundException($"{nameof(Album)} with Id '{invalidAlbumId}' not found."));

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(getAlbumByIdQuery, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(Album), invalidAlbumId, LogAction.Get), Times.Never());
    }
}
