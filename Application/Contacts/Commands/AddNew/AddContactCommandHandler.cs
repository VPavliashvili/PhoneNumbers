using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Contacts.Commands.AddNew;

public class AddContactCommandHandler : IRequestHandler<AddContactCommand, int>
{
    private readonly IContactsRepository _contactsRepository;

    public AddContactCommandHandler(IContactsRepository contactsRepository)
    {
        _contactsRepository = contactsRepository;
    }

    public async Task<int> Handle(AddContactCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var newContact = new Contact()
        {
            Name = request.Name,
            Surname = request.Surname,
            PhoneNumbers = request.MobileNumbers
        };
        try
        {
            var res = await _contactsRepository.AddContact(newContact);
            return res.Id;
        }
        catch
        {
            throw;
        }
    }
}