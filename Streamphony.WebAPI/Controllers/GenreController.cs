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
    public async Task<ActionResult<GenreDto>> CreateGenre(GenreCreationDto genreDto)
    {
        var createdGenreDto = await _mediator.Send(new CreateGenre(genreDto));
        return CreatedAtAction(nameof(GetGenreById), new { id = createdGenreDto.Id }, createdGenreDto);
    }

    [HttpPut]
    [ValidateModel]
    public async Task<ActionResult<GenreDto>> UpdateGenre(GenreDto genreDto)
    {
        try
        {
            var updatedGenreDto = await _mediator.Send(new UpdateGenre(genreDto));
            return Ok(updatedGenreDto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Genre with ID {genreDto.Id} not found.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres()
    {
        var genres = await _mediator.Send(new GetAllGenres());
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetGenreById(Guid id)
    {
        var genreDto = await _mediator.Send(new GetGenreById(id));
        if (genreDto == null) return NotFound("Genre not found.");
        return Ok(genreDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        var result = await _mediator.Send(new DeleteGenre(id));
        if (!result) return NotFound($"Genre with ID {id} not found.");
        return Ok();
    }
}
