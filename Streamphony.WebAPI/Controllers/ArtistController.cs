using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Artists.Commands;
using Streamphony.Application.App.Artists.Queries;
using Streamphony.Application.App.Artists.DTOs;
using Streamphony.Application.App.Auth.Commands;
using Streamphony.Application.App.BlobStorage.Commands;
using Streamphony.Application.Common;
using Streamphony.Application.Common.Enum;
using Streamphony.WebAPI.Extensions;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/artists")]
public class ArtistController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Policy = "UserPolicy")]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateArtist(ArtistCreationDto artistDto)
    {
        var userId = User.GetUserId();

        var createdArtistDto = await _mediator.Send(new CreateArtist(artistDto, userId));
        await _mediator.Send(new CommitBlob(artistDto.ProfilePictureId, userId, createdArtistDto.Id,
            BlobType.ProfilePicture.ToString()));
        var result = await _mediator.Send(new RefreshToken(userId));

        return Ok(result);
    }

    [HttpPut]
    [ValidateModel]
    [Authorize(Policy = "ArtistPolicy")]
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
    public async Task<ActionResult<PaginatedResult<ArtistDetailsDto>>> GetAllArtists([FromQuery] int pageNumber = 1,
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
    public async Task<ActionResult<PaginatedResult<ArtistDetailsDto>>> GetAllArtistsFiltered(PagedRequest pagedRequest)
    {
        var artists = await _mediator.Send(new GetAllArtists(pagedRequest));
        return Ok(artists);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArtistDetailsDto>> GetArtistById(Guid id)
    {
        var artistDto = await _mediator.Send(new GetArtistById(id));
        return Ok(artistDto);
    }

    [HttpGet("current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ArtistDetailsDto>> GetArtistDetailsForCurrentUser()
    {
        var userId = User.GetUserId();

        var artist = await _mediator.Send(new GetArtistForUser(userId));
        return Ok(artist);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "ArtistPolicy")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArtist(Guid id)
    {
        var userId = User.GetUserId();

        await _mediator.Send(new DeleteArtist(id, userId));
        return NoContent();
    }
}
