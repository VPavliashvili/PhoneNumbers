using Infrastructure.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContactsController> _logger;
    private readonly string _unicId;

    public ContactsController(IMediator mediator, ILogger<ContactsController> logger, LoggingIdWrapper loggingId)
    {
        _mediator = mediator;
        _logger = logger;
        _unicId = loggingId.UnicId;
    }

    //[HttpPost]
    //public Task<IActionResult> AddContact(AddContactCommandRequest request)
    //{
    //    _logger.LogInformation($"{nameof(AddContact)} request with unicId -> {_unicId}, body -> {{@request}}", request);

    //    _logger.LogInformation($"for {nameof(AddContact)} with unicId -> {_unicId}, new contact inserted successfully, id -> {result}");
    //}

}
