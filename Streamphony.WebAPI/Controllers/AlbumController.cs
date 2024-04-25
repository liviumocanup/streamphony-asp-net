using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Albums.Commands;
using Streamphony.Application.App.Albums.Queries;
using Streamphony.Application.App.Albums.Responses;

namespace Streamphony.WebAPI.Controllers
{
    [Route("api/albums")]
    public class AlbumController(IMediator mediator) : AppBaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<AlbumDto>> CreateAlbum(AlbumCreationDto albumDto)
        {
            var createdAlbumDto = await _mediator.Send(new CreateAlbum(albumDto));
            return CreatedAtAction(nameof(GetAlbumById), new { id = createdAlbumDto.Id }, createdAlbumDto);
        }

        [HttpPut]
        public async Task<ActionResult<AlbumDto>> UpdateAlbum(AlbumDto albumDto)
        {
            try
            {
                var updatedAlbumDto = await _mediator.Send(new UpdateAlbum(albumDto));
                return Ok(updatedAlbumDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAllAlbums()
        {
            var albums = await _mediator.Send(new GetAllAlbums());
            return Ok(albums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDto>> GetAlbumById(Guid id)
        {
            var albumDto = await _mediator.Send(new GetAlbumById(id));
            if (albumDto == null) return NotFound("Album not found.");
            return Ok(albumDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var result = await _mediator.Send(new DeleteAlbum(id));
            if (!result) return NotFound($"Album with ID {id} not found.");
            return Ok();
        }
    }
}
