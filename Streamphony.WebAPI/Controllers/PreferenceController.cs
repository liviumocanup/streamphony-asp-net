using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Preferences.Commands;
using Streamphony.Application.App.Preferences.Queries;
using Streamphony.Application.App.Preferences.Responses;
using Streamphony.Application.Common;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/preferences")]
[Authorize(Roles = "Admin")]
public class PreferenceController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PreferenceDto>> CreatePreference(PreferenceDto preferenceDto)
    {
        var createdPreferenceDto = await _mediator.Send(new CreatePreference(preferenceDto));
        return CreatedAtAction(nameof(CreatePreference), new { id = createdPreferenceDto.Id }, createdPreferenceDto);
    }

    [HttpPut]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PreferenceDto>> UpdatePreference(PreferenceDto preferenceDto)
    {
        var updatedPreferenceDto = await _mediator.Send(new UpdatePreference(preferenceDto));
        return Ok(updatedPreferenceDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PaginatedResult<PreferenceDto>>> GetAllPreferences([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagedRequest = new PagedRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var preferences = await _mediator.Send(new GetAllPreferences(pagedRequest));
        return Ok(preferences);
    }

    [HttpPost("filtered")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PaginatedResult<PreferenceDto>>> GetAllPreferencesFiltered(PagedRequest pagedRequest)
    {
        var preferences = await _mediator.Send(new GetAllPreferences(pagedRequest));
        return Ok(preferences);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PreferenceDto>> GetPreferenceById(Guid id)
    {
        var preferenceDto = await _mediator.Send(new GetPreferenceById(id));
        return Ok(preferenceDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePreference(Guid id)
    {
        await _mediator.Send(new DeletePreference(id));
        return NoContent();
    }
}
