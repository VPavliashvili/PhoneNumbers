using MediatR;

namespace Application.Contacts.Commands.AddNew;

public record AddContactCommand(AddContactCommandRequest Request) : IRequest<int>;
