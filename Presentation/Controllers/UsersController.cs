using Application.Users.Commands.Login;
using Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest request) 
    {
        var command = new RegisterUserCommand(request);
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(Register), new {result}, result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
    {
        var command = new LoginUserCommand(request);
        var jwt = await _mediator.Send(command);

        return Ok(jwt);
    }
}
