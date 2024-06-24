using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Auth.Commands;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.WebAPI.Extensions;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/auth")]
public class AuthController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    [AllowAnonymous]
    [ValidateModel]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        var result = await _mediator.Send(new RegisterUser(registerUserDto));

        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        var result = await _mediator.Send(new LoginUser(loginUserDto));

        return Ok(result);
    }
    
    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var userId = User.GetUserId();
        
        var result = await _mediator.Send(new RefreshToken(userId));

        return Ok(result);
    }
}
