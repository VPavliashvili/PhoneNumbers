using MediatR;

namespace Application.Contacts.Commands.Delete
{
    public record DeleteContactCommand(DeleteContactCommandRequest Request) : IRequest<bool>;
}
