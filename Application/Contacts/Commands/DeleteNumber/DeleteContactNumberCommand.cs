using MediatR;

namespace Application.Contacts.Commands.DeleteNumber;

public record DeleteContactNumberCommand(DeleteContactNumberCommandRequest Request) : IRequest<bool>;
