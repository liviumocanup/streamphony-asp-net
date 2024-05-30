using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.Auth.Commands;
using Streamphony.Application.App.Auth.Responses;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/auth")]
[AllowAnonymous]
public class AuthController(IMediator mediator) : AppBaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    [ValidateModel]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        var result = await _mediator.Send(new RegisterUser(registerUserDto));

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        var result = await _mediator.Send(new LoginUser(loginUserDto));

        return Ok(result);
    }
}