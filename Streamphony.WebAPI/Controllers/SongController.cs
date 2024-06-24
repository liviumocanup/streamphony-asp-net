using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.BlobStorage.Commands;
using Streamphony.Application.App.Songs.Commands;
using Streamphony.Application.App.Songs.Queries;
using Streamphony.Application.App.Songs.DTOs;
using Streamphony.Application.Common;
using Streamphony.Application.Common.Enum;
using Streamphony.WebAPI.Extensions;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/songs")]
[Authorize(Policy = "ArtistPolicy")]
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
    public async Task<ActionResult<SongDto>> CreateSong(SongCreationDto songCreationDto)
    {
        var userId = User.GetUserId();
        
        var createdSongDto = await _mediator.Send(new CreateSong(songCreationDto, userId));
        var coverUrl = await _mediator.Send(new CommitBlob(songCreationDto.CoverFileId, userId, createdSongDto.Id, BlobType.SongCover.ToString()));
        var audioUrl = await _mediator.Send(new CommitBlob(songCreationDto.AudioFileId, userId, createdSongDto.Id, BlobType.Song.ToString()));
        
        createdSongDto.CoverUrl = coverUrl;
        createdSongDto.AudioUrl = audioUrl;
        
        return CreatedAtAction(nameof(GetSongById), new { id = createdSongDto.Id }, createdSongDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SongDto>> UpdateSong(SongEditRequestDto songDto)
    {
        var userId = User.GetUserId();
        
        var updatedSongDto = await _mediator.Send(new UpdateSong(songDto, userId));
        await _mediator.Send(new CommitBlob(songDto.CoverBlobId, userId, songDto.Id, BlobType.SongCover.ToString()));
        
        return Ok(updatedSongDto);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<SongDetailsDto>>> GetAllSongs([FromQuery] int pageNumber = 1,
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
    public async Task<ActionResult<PaginatedResult<SongDetailsDto>>> GetAllSongsFiltered(PagedRequest pagedRequest)
    {
        var songs = await _mediator.Send(new GetAllSongs(pagedRequest));
        return Ok(songs);
    }
    
    [HttpGet("user/current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SongDetailsDto>>> GetSongsForCurrentUser()
    {
        var userId = User.GetUserId();

        var songs = await _mediator.Send(new GetSongsForArtist(userId));
        return Ok(songs);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SongDetailsDto>> GetSongById(Guid id)
    {
        var songDto = await _mediator.Send(new GetSongById(id));
        return Ok(songDto);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSong(Guid id)
    {
        var userId = User.GetUserId();
        
        await _mediator.Send(new DeleteSong(id, userId));
        return NoContent();
    }
}
