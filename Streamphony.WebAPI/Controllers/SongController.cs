using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Songs.Commands;
using Streamphony.Application.App.Songs.Queries;
using Streamphony.Application.App.Songs.Responses;

namespace Streamphony.WebAPI.Controllers
{
    [Route("api/songs")]
    public class SongController(IMediator mediator) : AppBaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SongDto>> CreateSong(SongDto songDto)
        {
            var createdSongDto = await _mediator.Send(new CreateSong(songDto));
            return CreatedAtAction(nameof(GetSongById), new { id = createdSongDto.Id }, createdSongDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            var result = await _mediator.Send(new DeleteSong(id));
            if (!result) return NotFound($"Song with ID {id} not found.");
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDto>>> GetAllSongs()
        {
            var songs = await _mediator.Send(new GetAllSongs());
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongDto>> GetSongById(Guid id)
        {
            var songDto = await _mediator.Send(new GetSongById(id));
            if (songDto == null) return NotFound("Song not found.");
            return Ok(songDto);
        }
    }
}