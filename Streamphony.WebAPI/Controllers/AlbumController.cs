using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Albums.Commands;
using Streamphony.Application.App.Albums.Queries;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/albums")]
public class AlbumController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AlbumDto>> CreateAlbum(AlbumCreationDto albumDto)
    {
        var createdAlbumDto = await _mediator.Send(new CreateAlbum(albumDto));
        return CreatedAtAction(nameof(GetAlbumById), new { id = createdAlbumDto.Id }, createdAlbumDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AlbumDto>> UpdateAlbum(AlbumDto albumDto)
    {
        var updatedAlbumDto = await _mediator.Send(new UpdateAlbum(albumDto));
        return Ok(updatedAlbumDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAllAlbums()
    {
        var albums = await _mediator.Send(new GetAllAlbums());
        return Ok(albums);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AlbumDto>> GetAlbumById(Guid id)
    {
        var albumDto = await _mediator.Send(new GetAlbumById(id));
        return Ok(albumDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAlbum(Guid id)
    {
        await _mediator.Send(new DeleteAlbum(id));
        return NoContent();
    }
}
