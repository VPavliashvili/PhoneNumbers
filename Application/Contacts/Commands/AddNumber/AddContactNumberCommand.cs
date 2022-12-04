using MediatR;

namespace Application.Contacts.Commands.AddNumber
{
    public record AddContactNumberCommand(AddContactNumberCommandRequest Request) : IRequest<int>;
}
