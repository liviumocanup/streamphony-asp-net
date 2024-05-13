using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Users.Commands;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Common;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/users")]
public class UserController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDto>> CreateUser(UserCreationDto userDto)
    {
        var createdUserDto = await _mediator.Send(new CreateUser(userDto));
        return CreatedAtAction(nameof(GetUserById), new { id = createdUserDto.Id }, createdUserDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDto>> UpdateUser(UserDto userDto)
    {
        var updatedUserDto = await _mediator.Send(new UpdateUser(userDto));
        return Ok(updatedUserDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<UserDto>>> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var users = await _mediator.Send(new GetAllUsers(pageNumber, pageSize));
        return Ok(users);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetailsDto>> GetUserById(Guid id)
    {
        var userDto = await _mediator.Send(new GetUserById(id));
        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _mediator.Send(new DeleteUser(id));
        return NoContent();
    }
}
