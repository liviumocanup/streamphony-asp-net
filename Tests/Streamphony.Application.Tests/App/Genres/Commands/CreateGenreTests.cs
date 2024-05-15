using Moq;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Genres.Commands;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.Application.Exceptions;
using Streamphony.Application.Services;
using Streamphony.Domain.Models;

namespace Streamphony.Application.Tests.App.Genres.Commands;

[TestFixture]
public class CreateGenreTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMappingProvider> _mapperMock;
    private Mock<ILoggingService> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private CreateGenreHandler _handler;
    private GenreCreationDto _genreCreationDto;
    private Genre _genreEntity;
    private GenreDto _genreDto;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMappingProvider>();
        _loggerMock = new Mock<ILoggingService>();
        _validationServiceMock = new Mock<IValidationService>();
        _handler = new CreateGenreHandler(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object, _validationServiceMock.Object);

        _genreCreationDto = new GenreCreationDto
        {
            Name = "Rock",
            Description = "A genre of popular music."
        };

        _genreEntity = new Genre
        {
            Id = Guid.NewGuid(),
            Name = _genreCreationDto.Name,
            Description = _genreCreationDto.Description
        };

        _genreDto = new GenreDto
        {
            Id = _genreEntity.Id,
            Name = _genreCreationDto.Name,
            Description = _genreCreationDto.Description
        };

        _unitOfWorkMock.Setup(u => u.GenreRepository.GetByName(_genreCreationDto.Name, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Genre)null!);
        _mapperMock.Setup(m => m.Map<Genre>(_genreCreationDto)).Returns(_genreEntity);
        _unitOfWorkMock.Setup(u => u.GenreRepository.Add(_genreEntity, It.IsAny<CancellationToken>())).ReturnsAsync(_genreEntity);
        _mapperMock.Setup(m => m.Map<GenreDto>(_genreEntity)).Returns(_genreDto);
    }

    [Test]
    public async Task Handle_GenreIsUnique_CreatesGenreSuccessfully()
    {
        // Arrange
        var createGenreCommand = new CreateGenre(_genreCreationDto);

        // Act
        var result = await _handler.Handle(createGenreCommand, CancellationToken.None);

        // Assert
        _validationServiceMock.Verify(v => v.EnsureUniqueProperty(It.IsAny<Func<string, CancellationToken, Task<Genre?>>>(), nameof(_genreCreationDto.Name), _genreCreationDto.Name, It.IsAny<CancellationToken>(), LogAction.Create), Times.Once());
        _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), _genreEntity.Id, LogAction.Create), Times.Once());
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(_genreDto.Id));
            Assert.That(result.Name, Is.EqualTo(_genreDto.Name));
        });
    }

    [Test]
    public void Handle_GenreNameNotUnique_ThrowsDuplicateException()
    {
        // Arrange
        var createGenreCommand = new CreateGenre(_genreCreationDto);
        var duplicateException = new DuplicateException("A genre with the same name already exists.");

        _validationServiceMock.Setup(v => v.EnsureUniqueProperty(It.IsAny<Func<string, CancellationToken, Task<Genre?>>>(), nameof(_genreCreationDto.Name), _genreCreationDto.Name, CancellationToken.None, LogAction.Create))
                              .ThrowsAsync(duplicateException);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateException>(() => _handler.Handle(createGenreCommand, CancellationToken.None));
        _unitOfWorkMock.Verify(u => u.GenreRepository.Add(It.IsAny<Genre>(), It.IsAny<CancellationToken>()), Times.Never());
        _loggerMock.Verify(l => l.LogSuccess(nameof(Genre), It.IsAny<Guid>(), LogAction.Create), Times.Never());
    }
}
