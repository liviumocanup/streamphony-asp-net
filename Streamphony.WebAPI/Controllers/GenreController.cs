using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Genres.Commands;
using Streamphony.Application.App.Genres.Queries;
using Streamphony.Application.App.Genres.Responses;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/genres")]
public class GenreController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GenreDto>> CreateGenre(GenreCreationDto genreDto)
    {
        var createdGenreDto = await _mediator.Send(new CreateGenre(genreDto));
        return CreatedAtAction(nameof(GetGenreById), new { id = createdGenreDto.Id }, createdGenreDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GenreDto>> UpdateGenre(GenreDto genreDto)
    {
        var updatedGenreDto = await _mediator.Send(new UpdateGenre(genreDto));
        return Ok(updatedGenreDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres()
    {
        var genres = await _mediator.Send(new GetAllGenres());
        return Ok(genres);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenreDto>> GetGenreById(Guid id)
    {
        var genreDto = await _mediator.Send(new GetGenreById(id));
        return Ok(genreDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        await _mediator.Send(new DeleteGenre(id));
        return NoContent();
    }
}
