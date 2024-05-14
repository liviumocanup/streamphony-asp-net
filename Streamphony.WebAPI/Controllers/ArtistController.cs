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
    [Authorize]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ArtistDto>> CreateArtist(ArtistCreationDto artistDto)
    {
        var createdArtistDto = await _mediator.Send(new CreateArtist(artistDto));
        return CreatedAtAction(nameof(GetArtistById), new { id = createdArtistDto.Id }, createdArtistDto);
    }

    [HttpPut]
    [Authorize]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<ArtistDto>>> GetAllArtists([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var artists = await _mediator.Send(new GetAllArtists(pageNumber, pageSize));
        return Ok(artists);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArtistDetailsDto>> GetArtistById(Guid id)
    {
        var artistDto = await _mediator.Send(new GetArtistById(id));
        return Ok(artistDto);
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArtist(Guid id)
    {
        await _mediator.Send(new DeleteArtist(id));
        return NoContent();
    }
}
