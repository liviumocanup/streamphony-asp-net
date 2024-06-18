using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Artists.Commands;
using Streamphony.Application.App.Artists.Queries;
using Streamphony.Application.App.Artists.Responses;
using Streamphony.Application.Common;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/artists")]
public class ArtistController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ExtractUserId]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ArtistDto>> CreateArtist(ArtistCreationDto artistDto)
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized("ID is missing from the context");
        
        var createdArtistDto = await _mediator.Send(new CreateArtist(artistDto, userId));
        return CreatedAtAction(nameof(CreateArtist), new { id = createdArtistDto.Id }, createdArtistDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArtistDto>> UpdateArtist(ArtistDto artistDto)
    {
        var updatedArtistDto = await _mediator.Send(new UpdateArtist(artistDto));
        return Ok(updatedArtistDto);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<ArtistDto>>> GetAllArtists([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagedRequest = new PagedRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var artists = await _mediator.Send(new GetAllArtists(pagedRequest));
        return Ok(artists);
    }

    [HttpPost("filtered")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<ArtistDto>>> GetAllArtistsFiltered(PagedRequest pagedRequest)
    {
        var artists = await _mediator.Send(new GetAllArtists(pagedRequest));
        return Ok(artists);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArtistDetailsDto>> GetArtistById(Guid id)
    {
        var artistDto = await _mediator.Send(new GetArtistById(id));
        return Ok(artistDto);
    }

    [HttpDelete("{id}")]
    [ExtractUserId]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArtist(Guid id)
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized();
        
        await _mediator.Send(new DeleteArtist(id, userId));
        return NoContent();
    }
}
