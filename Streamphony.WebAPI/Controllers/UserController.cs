using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Users.Commands;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.App.Users.Responses;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/users")]
public class UserController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ValidateModel]
    public async Task<ActionResult<UserDto>> CreateUser(UserCreationDto userDto)
    {
        var createdUserDto = await _mediator.Send(new CreateUser(userDto));
        return CreatedAtAction(nameof(GetUserById), new { id = createdUserDto.Id }, createdUserDto);
    }

    [HttpPut]
    [ValidateModel]
    public async Task<ActionResult<UserDto>> UpdateUser(UserDto userDto)
    {
        try
        {
            var updatedUserDto = await _mediator.Send(new UpdateUser(userDto));
            return Ok(updatedUserDto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"User with ID {userDto.Id} not found.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsers());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailsDto>> GetUserById(Guid id)
    {
        var userDto = await _mediator.Send(new GetUserById(id));
        if (userDto == null) return NotFound("User not found.");
        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await _mediator.Send(new DeleteUser(id));
        if (!result) return NotFound($"User with ID {id} not found.");
        return Ok();
    }
}
