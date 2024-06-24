using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Commands;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Common;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;
using Streamphony.Domain.Models.Auth;

namespace Streamphony.Application.Tests.App.Albums.Commands;

[TestFixture]
public class CreateAlbumTests
{
    [SetUp]
    public void Setup()
    {
        _albumCreationDto = new AlbumCreationDto
        {
            Title = "New Album",
            CoverImageUrl = "http://example.com/cover.jpg",
            ReleaseDate = new DateOnly(2023, 1, 1),
            OwnerId = Guid.NewGuid()
        };

        _albumEntity = new Album
        {
            Id = Guid.NewGuid(),
            Title = _albumCreationDto.Title,
            CoverImageUrl = _albumCreationDto.CoverImageUrl,
            ReleaseDate = _albumCreationDto.ReleaseDate,
            OwnerId = _albumCreationDto.OwnerId
        };

        _albumDto = new AlbumDto
        {
            Id = _albumEntity.Id,
            Title = _albumEntity.Title,
            CoverImageUrl = _albumEntity.CoverImageUrl,
            ReleaseDate = _albumEntity.ReleaseDate,
            OwnerId = _albumEntity.OwnerId
        };

        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new CreateAlbumHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object,
            _validationServiceMock.Object);
    }

    private AlbumCreationDto _albumCreationDto;
    private Album _albumEntity;
    private AlbumDto _albumDto;

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private CreateAlbumHandler _handler;

    [Test]
    public async Task Handle_SuccessfulCreation_ReturnsAlbumDto()
    {
        // Arrange
        var createAlbumCommand = new CreateAlbum(_albumCreationDto);
        var createdAlbum = _albumEntity;

        _mapperMock.Setup(m => m.Map<Album>(_albumCreationDto)).Returns(createdAlbum);
        _unitOfWorkMock.Setup(u => u.AlbumRepository.Add(createdAlbum, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdAlbum);
        _mapperMock.Setup(m => m.Map<AlbumDto>(createdAlbum)).Returns(_albumDto);

        // Act
        var result = await _handler.Handle(createAlbumCommand, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(createdAlbum.Id));
            Assert.That(result.Title, Is.EqualTo(createdAlbum.Title));
        });
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Album), createdAlbum.Id, LogAction.Create), Times.Once());
    }

    [Test]
    public void Handle_OwnerDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var createAlbumCommand = new CreateAlbum(_albumCreationDto);
        var ownerId = _albumCreationDto.OwnerId;
        var exception = new NotFoundException("Owner not found.");

        _unitOfWorkMock
            .Setup(
                u => u.AlbumRepository.GetByOwnerIdAndTitle(ownerId, _albumCreationDto.Title, CancellationToken.None))
            .ReturnsAsync((Album)null!);
        _validationServiceMock.Setup(v =>
                v.AssertNavigationEntityExists<Album, User>(_unitOfWorkMock.Object.UserRepository, ownerId,
                    CancellationToken.None, LogAction.Create))
            .ThrowsAsync(exception);

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(
            () => _handler.Handle(createAlbumCommand, CancellationToken.None));
        Assert.That(ex, Is.EqualTo(exception));
    }
}
