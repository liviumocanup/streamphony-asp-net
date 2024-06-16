using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.App.Users.Responses;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/users")]
public class UserController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ExtractUserId]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized("ID is missing from the context");
        
        var userDto = await _mediator.Send(new GetUserById(userId));
        return Ok(userDto);
    }
}
