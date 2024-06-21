using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Albums.Commands;
using Streamphony.Application.App.Albums.Queries;
using Streamphony.Application.App.Albums.Responses;
using Streamphony.Application.Common;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/albums")]
[Authorize(Policy = "ArtistPolicy")]
public class AlbumController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AlbumDto>> CreateAlbum(AlbumCreationDto albumDto)
    {
        var createdAlbumDto = await _mediator.Send(new CreateAlbum(albumDto));
        return CreatedAtAction(nameof(CreateAlbum), new { id = createdAlbumDto.Id }, createdAlbumDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AlbumDto>> UpdateAlbum(AlbumDto albumDto)
    {
        var updatedAlbumDto = await _mediator.Send(new UpdateAlbum(albumDto));
        return Ok(updatedAlbumDto);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<AlbumDto>>> GetAllAlbums([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagedRequest = new PagedRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var albums = await _mediator.Send(new GetAllAlbums(pagedRequest));
        return Ok(albums);
    }

    [HttpPost("filtered")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<AlbumDto>>> GetAllAlbumsFiltered(PagedRequest pagedRequest)
    {
        var albums = await _mediator.Send(new GetAllAlbums(pagedRequest));
        return Ok(albums);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AlbumDto>> GetAlbumById(Guid id)
    {
        var albumDto = await _mediator.Send(new GetAlbumById(id));
        return Ok(albumDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAlbum(Guid id)
    {
        await _mediator.Send(new DeleteAlbum(id));
        return NoContent();
    }
}
