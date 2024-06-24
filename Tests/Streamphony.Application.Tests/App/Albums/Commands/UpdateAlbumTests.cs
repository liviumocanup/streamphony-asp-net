using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Albums.Commands;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Albums.Commands;

[TestFixture]
public class UpdateAlbumTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new UpdateAlbumHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object,
            _validationServiceMock.Object);

        _albumOwner = new User
        {
            Id = Guid.NewGuid(),
            Username = "user1",
            Email = "user@mail.com",
            ArtistName = "Artist",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        _albumDto = new AlbumDto
        {
            Id = Guid.NewGuid(),
            Title = "Updated Title",
            OwnerId = _albumOwner.Id
        };

        _existingAlbum = new Album
        {
            Id = _albumDto.Id,
            Title = "Original Title",
            OwnerId = _albumOwner.Id
        };

        _albumOwner.OwnedAlbums.Add(_existingAlbum);

        _unitOfWorkMock.Setup(u => u.AlbumRepository.GetById(_albumDto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_existingAlbum);
        _unitOfWorkMock.Setup(u => u.UserRepository.GetById(_albumDto.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_albumOwner);
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.AlbumRepository, _albumDto.Id,
                It.IsAny<CancellationToken>(), It.IsAny<LogAction>()))
            .ReturnsAsync(_existingAlbum);
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserRepository, _albumDto.OwnerId,
                It.IsAny<CancellationToken>(), It.IsAny<LogAction>()))
            .ReturnsAsync(_albumOwner);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private UpdateAlbumHandler _handler;
    private AlbumDto _albumDto;
    private Album _existingAlbum;
    private User _albumOwner;

    [Test]
    public async Task Handle_AlbumUpdate_SuccessfullyUpdatesAlbum()
    {
        // Arrange
        var updateAlbumCommand = new UpdateAlbum(_albumDto);

        _mapperMock.Setup(m => m.Map(_albumDto, _existingAlbum)).Verifiable();
        _mapperMock.Setup(m => m.Map<AlbumDto>(_existingAlbum)).Returns(_albumDto);

        // Act
        var result = await _handler.Handle(updateAlbumCommand, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map(_albumDto, _existingAlbum), Times.Once());
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Album), _existingAlbum.Id, LogAction.Update), Times.Once());
        Assert.That(result, Is.EqualTo(_albumDto));
    }

    [Test]
    public void Handle_UserDoesNotOwnAlbum_ThrowsNotAuthorizedException()
    {
        // Arrange
        var differentUser = new User
        {
            Id = Guid.NewGuid(),
            Username = "name2",
            ArtistName = "Different Artist",
            Email = "diff@mail.com",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };
        _albumDto.OwnerId = differentUser.Id;
        _validationServiceMock.Setup(v => v.GetExistingEntity(_unitOfWorkMock.Object.UserRepository, _albumDto.OwnerId,
                It.IsAny<CancellationToken>(), It.IsAny<LogAction>()))
            .ReturnsAsync(differentUser);
        _loggerMock.Setup(l =>
                l.LogAndThrowNotAuthorizedException(nameof(Album), _albumDto.Id, nameof(User), _albumDto.OwnerId,
                    LogAction.Update))
            .Throws(new UnauthorizedException("User does not own album."));

        var updateAlbumCommand = new UpdateAlbum(_albumDto);

        // Act & Assert
        Assert.ThrowsAsync<UnauthorizedException>(() => _handler.Handle(updateAlbumCommand, CancellationToken.None));
        _loggerMock.Verify(
            l => l.LogAndThrowNotAuthorizedException(nameof(Album), _albumDto.Id, nameof(User), _albumDto.OwnerId,
                LogAction.Update), Times.Once());
    }

    [Test]
    public void Handle_DuplicateTitleForOtherAlbum_ThrowsDuplicateException()
    {
        // Arrange
        var duplicateTitleFunc = _unitOfWorkMock.Object.AlbumRepository.GetByOwnerIdAndTitleWhereIdNotEqual;
        var duplicateException = new DuplicateException("Duplicate title for another album.");

        _validationServiceMock.Setup(v => v.EnsureUserUniquePropertyExceptId(duplicateTitleFunc, _albumDto.OwnerId,
                nameof(_albumDto.Title), _albumDto.Title, _albumDto.Id, It.IsAny<CancellationToken>(),
                LogAction.Update))
            .ThrowsAsync(duplicateException);

        var updateAlbumCommand = new UpdateAlbum(_albumDto);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(updateAlbumCommand, CancellationToken.None));
        _loggerMock.Verify(
            l => l.LogAndThrowDuplicateException(nameof(Album), nameof(_albumDto.Title), _albumDto.Title,
                LogAction.Update), Times.Never());
    }
}
