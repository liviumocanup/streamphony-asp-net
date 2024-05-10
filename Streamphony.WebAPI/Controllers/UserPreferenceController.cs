using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.UserPreferences.Commands;
using Streamphony.Application.App.UserPreferences.Queries;
using Streamphony.Application.App.UserPreferences.Responses;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/userPreferences")]
public class UserPreferenceController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserPreferenceDto>> CreateUserPreference(UserPreferenceDto userPreferenceDto)
    {
        var createdUserPreferenceDto = await _mediator.Send(new CreateUserPreference(userPreferenceDto));
        return CreatedAtAction(nameof(GetUserPreferenceById), new { id = createdUserPreferenceDto.Id }, createdUserPreferenceDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserPreferenceDto>> UpdateUserPreference(UserPreferenceDto userPreferenceDto)
    {
        var updatedUserPreferenceDto = await _mediator.Send(new UpdateUserPreference(userPreferenceDto));
        return Ok(updatedUserPreferenceDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserPreferenceDto>>> GetAllUserPreferences()
    {
        var userPreferences = await _mediator.Send(new GetAllUserPreferences());
        return Ok(userPreferences);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserPreferenceDto>> GetUserPreferenceById(Guid id)
    {
        var userPreferenceDto = await _mediator.Send(new GetUserPreferenceById(id));
        return Ok(userPreferenceDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserPreference(Guid id)
    {
        await _mediator.Send(new DeleteUserPreference(id));
        return NoContent();
    }
}
