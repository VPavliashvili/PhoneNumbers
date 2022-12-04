using MediatR;

namespace Application.Contacts.Commands.Update;

public record UpdateContactCommand(UpdateContactCommandRequest Request) : IRequest<UpdateContactCommandResult>;
