using Application.Contacts.Commands.AddNew;
using Application.Contacts.Commands.Delete;
using Infrastructure.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpPost]
    [Authorize]
    [Route("New")]
    public async Task<IActionResult> AddNewContact(AddContactCommandRequest request)
    {
        _logger.LogInformation($"{nameof(AddNewContact)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new AddContactCommand(request);
        var result = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(AddNewContact)} with unicId -> {_unicId}, new contact inserted successfully, id -> {result}");

        return Created("/Contacts/New", result);
    }

    [HttpDelete]
    [Authorize]
    [Route("Delete")]
    public async Task<IActionResult> DeleteContact(DeleteContactCommandRequest request)
    {
        _logger.LogInformation($"{nameof(DeleteContact)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new DeleteContactCommand(request);
        var result = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(DeleteContact)} with unicId -> {_unicId}, contact deleted successfully with id -> {request.Id}");

        return NoContent();
    }

}
