using Application.Users.Commands.Login;
using Application.Users.Commands.RegisterUser;
using Infrastructure.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;
    private readonly string _unicId;

    public UsersController(IMediator mediator, ILogger<UsersController> logger, LoggingIdWrapper loggingId)
    {
        _mediator = mediator;
        _logger = logger;
        _unicId = loggingId.UnicId;
    }

    [HttpPost]
    [Route("User")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest request) 
    {
        _logger.LogInformation($"{nameof(Register)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new RegisterUserCommand(request);
        var result = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(Register)} with unicId -> {_unicId}, user registered successfully, id -> {result}");

        return Created("/User/", new {result});
    }

    [HttpPost]
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
    {
        _logger.LogInformation($"{nameof(Login)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new LoginUserCommand(request);
        var jwt = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(Login)} with unicId -> {_unicId}, jwt generated successfully -> {jwt}");

        return Ok(jwt);
    }
}
