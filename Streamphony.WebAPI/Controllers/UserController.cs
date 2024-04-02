using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Users.Commands;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Users.Queries;

namespace Streamphony.WebAPI.Controllers
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsers());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var userDto = await _mediator.Send(new GetUserById(id));
            if (userDto == null) return NotFound("User not found.");
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            var createdUserDto = await _mediator.Send(new CreateUser(userDto));
            return CreatedAtAction(nameof(GetUserById), new { id = createdUserDto.Id }, createdUserDto);
        }
    }
}
