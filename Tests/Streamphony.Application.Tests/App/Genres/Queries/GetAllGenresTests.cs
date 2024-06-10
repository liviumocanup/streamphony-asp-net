using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Queries;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Genres.Queries;

[TestFixture]
public class GetAllGenresTests
{
    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _handler = new GetAllGenresHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);

        _genres =
        [
            new Genre { Id = Guid.NewGuid(), Name = "Rock", Description = "A genre of popular music." },
            new Genre { Id = Guid.NewGuid(), Name = "Jazz", Description = "A music genre." }
        ];

        _genreDtos = _genres.Select(genre => new GenreDto
            { Id = genre.Id, Name = genre.Name, Description = genre.Description }).ToList();

        _unitOfWorkMock.Setup(u => u.GenreRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(_genres);
        _mapperMock.Setup(m => m.Map<IEnumerable<GenreDto>>(_genres)).Returns(_genreDtos);
    }

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private GetAllGenresHandler _handler;
    private List<Genre> _genres;
    private List<GenreDto> _genreDtos;

    [Test]
    public async Task Handle_GenresExist_ReturnsAllGenreDtos()
    {
        // Arrange
        var getAllGenresQuery = new GetAllGenres();

        // Act
        var result = await _handler.Handle(getAllGenresQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(_genreDtos.Count));
            Assert.That(result, Is.EquivalentTo(_genreDtos));
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), LogAction.Get), Times.Once());
    }

    [Test]
    public async Task Handle_NoGenresExist_ReturnsEmpty()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.GenreRepository.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Genre>());
        var getAllGenresQuery = new GetAllGenres();

        // Act
        var result = await _handler.Handle(getAllGenresQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        });
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), LogAction.Get), Times.Once());
    }
}
