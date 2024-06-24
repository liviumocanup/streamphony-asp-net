using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Albums.Commands;
using Streamphony.Application.App.Albums.Queries;
using Streamphony.Application.App.Albums.DTOs;
using Streamphony.Application.App.BlobStorage.Commands;
using Streamphony.Application.Common;
using Streamphony.Application.Common.Enum;
using Streamphony.WebAPI.Extensions;
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
        var userId = User.GetUserId();
        
        var createdAlbumDto = await _mediator.Send(new CreateAlbum(albumDto, userId));
        var coverUrl = await _mediator.Send(new CommitBlob(albumDto.CoverFileId, userId, createdAlbumDto.Id, BlobType.AlbumCover.ToString()));

        createdAlbumDto.CoverUrl = coverUrl;
        
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
    public async Task<ActionResult<PaginatedResult<AlbumDetailsDto>>> GetAllAlbums([FromQuery] int pageNumber = 1,
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
    public async Task<ActionResult<PaginatedResult<AlbumDetailsDto>>> GetAllAlbumsFiltered(PagedRequest pagedRequest)
    {
        var albums = await _mediator.Send(new GetAllAlbums(pagedRequest));
        return Ok(albums);
    }
    
    [HttpGet("user/current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AlbumDetailsDto>>> GetAlbumsForCurrentUser()
    {
        var userId = User.GetUserId();

        var albums = await _mediator.Send(new GetAlbumsForUser(userId));
        return Ok(albums);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AlbumDetailsDto>> GetAlbumById(Guid id)
    {
        var albumResponse = await _mediator.Send(new GetAlbumById(id));
        return Ok(albumResponse);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAlbum(Guid id)
    {
        await _mediator.Send(new DeleteAlbum(id));
        return NoContent();
    }
}
