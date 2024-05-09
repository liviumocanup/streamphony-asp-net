using System.Linq.Expressions;
using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Queries;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Genres.Queries;

[TestFixture]
public class GetGenreByIdTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private GetGenreByIdHandler _handler;
    private Genre _existingGenre;
    private GenreDetailsDto _genreDetailsDto;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new GetGenreByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _existingGenre = new Genre
        {
            Id = Guid.NewGuid(),
            Name = "Rock",
            Description = "A popular music genre.",
            Songs = []
        };

        _genreDetailsDto = new GenreDetailsDto
        {
            Id = _existingGenre.Id,
            Name = _existingGenre.Name,
            Description = _existingGenre.Description,
            Songs = []
        };

        _unitOfWorkMock.Setup(u => u.GenreRepository.GetByIdWithInclude(_existingGenre.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_existingGenre);

        _validationServiceMock.Setup(v => v.GetExistingEntityWithInclude(
            _unitOfWorkMock.Object.GenreRepository.GetByIdWithInclude,
            _existingGenre.Id,
            LogAction.Get,
            It.IsAny<CancellationToken>(),
            It.IsAny<Expression<Func<Genre, object>>[]>()))
            .ReturnsAsync(_existingGenre);

        _mapperMock.Setup(m => m.Map<GenreDetailsDto>(_existingGenre)).Returns(_genreDetailsDto);
    }

    [Test]
    public async Task Handle_ValidId_ReturnsGenreDetailsDto()
    {
        // Arrange
        var getGenreByIdQuery = new GetGenreById(_existingGenre.Id);

        // Act
        var result = await _handler.Handle(getGenreByIdQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(_genreDetailsDto.Id));
            Assert.That(result.Name, Is.EqualTo(_genreDetailsDto.Name));
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), _existingGenre.Id, LogAction.Get), Times.Once());
    }

    [Test]
    public void Handle_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var newId = Guid.NewGuid();
        var getGenreByIdQuery = new GetGenreById(newId);

        _unitOfWorkMock.Setup(u => u.GenreRepository.GetByIdWithInclude(newId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Genre)null!);

        _validationServiceMock.Setup(v => v.GetExistingEntityWithInclude(
            _unitOfWorkMock.Object.GenreRepository.GetByIdWithInclude,
            newId,
            LogAction.Get,
            It.IsAny<CancellationToken>(),
            It.IsAny<Expression<Func<Genre, object>>[]>()))
            .ThrowsAsync(new NotFoundException("Genre not found."));

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(getGenreByIdQuery, CancellationToken.None));
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), newId, LogAction.Get), Times.Never());
    }
}
