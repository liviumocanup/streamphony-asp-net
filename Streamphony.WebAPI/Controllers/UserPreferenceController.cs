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
    public async Task<ActionResult<UserPreferenceDto>> CreateUserPreference(UserPreferenceDto userPreferenceDto)
    {
        var createdUserPreferenceDto = await _mediator.Send(new CreateUserPreference(userPreferenceDto));
        return CreatedAtAction(nameof(GetUserPreferenceById), new { id = createdUserPreferenceDto.Id }, createdUserPreferenceDto);
    }

    [HttpPut]
    [ValidateModel]
    public async Task<ActionResult<UserPreferenceDto>> UpdateUserPreference(UserPreferenceDto userPreferenceDto)
    {
        try
        {
            var updatedUserPreferenceDto = await _mediator.Send(new UpdateUserPreference(userPreferenceDto));
            return Ok(updatedUserPreferenceDto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"UserPreference with ID {userPreferenceDto.Id} not found.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserPreferenceDto>>> GetAllUserPreferences()
    {
        var userPreferences = await _mediator.Send(new GetAllUserPreferences());
        return Ok(userPreferences);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserPreferenceDto>> GetUserPreferenceById(Guid id)
    {
        var userPreferenceDto = await _mediator.Send(new GetUserPreferenceById(id));
        if (userPreferenceDto == null) return NotFound("UserPreference not found.");
        return Ok(userPreferenceDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserPreference(Guid id)
    {
        var result = await _mediator.Send(new DeleteUserPreference(id));
        if (!result) return NotFound($"UserPreference with ID {id} not found.");
        return Ok();
    }
}
