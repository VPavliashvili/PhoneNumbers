using Application.Contacts.Commands.AddNew;
using Application.Contacts.Commands.AddNumber;
using Application.Contacts.Commands.Delete;
using Application.Contacts.Commands.DeleteNumber;
using Application.Contacts.Commands.Update;
using Application.Contacts.Queries.FindContact;
using Infrastructure.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[ApiController]
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
    [Route("Contact")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddNewContact(AddContactCommandRequest request)
    {
        _logger.LogInformation($"{nameof(AddNewContact)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new AddContactCommand(request);
        var result = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(AddNewContact)} with unicId -> {_unicId}, new contact inserted successfully, id -> {result}");

        return Created("/Contacts/", result);
    }

    [HttpDelete]
    [Authorize]
    [Route("Contact/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var request = new DeleteContactCommandRequest(id);

        _logger.LogInformation($"{nameof(DeleteContact)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new DeleteContactCommand(request);
        var result = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(DeleteContact)} with unicId -> {_unicId}, contact deleted successfully with id -> {request.Id}");

        return NoContent();
    }

    [HttpPut]
    [Authorize]
    [Route("Contact/{id}/New/{number}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddNewMobileNumber(int id, string number)
    {
        var request = new AddContactNumberCommandRequest(id, number);

        _logger.LogInformation($"{nameof(AddNewMobileNumber)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new AddContactNumberCommand(request);
        var result = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(AddNewMobileNumber)} with unicId -> {_unicId}, new number added successfully on contact id -> {request.Id}");

        return Created($"/Contact/{id}/New/{number}", result);
    }

    [HttpDelete]
    [Authorize]
    [Route("Contact/{id}/{number}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteMobileNumber(int id, string number)
    {
        var request = new DeleteContactNumberCommandRequest(id, number);

        _logger.LogInformation($"{nameof(DeleteMobileNumber)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new DeleteContactNumberCommand(request);
        var result = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(DeleteMobileNumber)} with unicId -> {_unicId}, number deleted successfully on contact id -> {request.Id}");

        return NoContent();
    }

    [HttpPatch]
    [Authorize]
    [Route("Contact/Update")]
    [ProducesResponseType(typeof(UpdateContactCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateContact(UpdateContactCommandRequest request)
    {
        _logger.LogInformation($"{nameof(UpdateContact)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var command = new UpdateContactCommand(request);
        var result = await _mediator.Send(command);

        _logger.LogInformation($"for {nameof(UpdateContact)} with unicId -> {_unicId}, contact updated successfully. new body -> {result}");

        return Ok(result);
    }

    [HttpGet]
    [Route("Contacts/{number}")]
    [ProducesResponseType(typeof(GetContactByNumberQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FindNumberOwner(string number)
    {
        var request = new GetContactByNumberQuery(number);

        _logger.LogInformation($"{nameof(FindNumberOwner)} request with unicId -> {_unicId}, body -> {{@request}}", request);

        var query = new GetContactByNumberQuery(number);
        var result = await _mediator.Send(query);

        _logger.LogInformation($"for {nameof(FindNumberOwner)} with unicId -> {_unicId}, from {number} found contact -> {result}");

        return Ok(result);
    }

}
