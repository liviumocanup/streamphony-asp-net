using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Songs.Commands;
using Streamphony.Application.App.Songs.Queries;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.Common;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/songs")]
public class SongController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SongDto>> CreateSong(SongCreationDto songDto)
    {
        var createdSongDto = await _mediator.Send(new CreateSong(songDto));
        return CreatedAtAction(nameof(GetSongById), new { id = createdSongDto.Id }, createdSongDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SongDto>> UpdateSong(SongDto songDto)
    {
        var updatedSongDto = await _mediator.Send(new UpdateSong(songDto));
        return Ok(updatedSongDto);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<SongDto>>> GetAllSongs([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagedRequest = new PagedRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var songs = await _mediator.Send(new GetAllSongs(pagedRequest));
        return Ok(songs);
    }

    [HttpPost("filtered")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<SongDto>>> GetAllSongsFiltered(PagedRequest pagedRequest)
    {
        var songs = await _mediator.Send(new GetAllSongs(pagedRequest));
        return Ok(songs);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SongDto>> GetSongById(Guid id)
    {
        var songDto = await _mediator.Send(new GetSongById(id));
        return Ok(songDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSong(Guid id)
    {
        await _mediator.Send(new DeleteSong(id));
        return NoContent();
    }
}
