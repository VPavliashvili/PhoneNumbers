using Domain.Abstractions;
using MediatR;

namespace Application.Contacts.Commands.DeleteNumber;

public class DeleteContactNumberCommandHandler : IRequestHandler<DeleteContactNumberCommand, bool>
{

    private readonly IContactsRepository _contactsRepository;

    public DeleteContactNumberCommandHandler(IContactsRepository contactsRepository)
    {
        _contactsRepository = contactsRepository;
    }

    public async Task<bool> Handle(DeleteContactNumberCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var res = await _contactsRepository.DeleteMobileNumberFromContact(request.Id, request.Number);

        return res;
    }
}
