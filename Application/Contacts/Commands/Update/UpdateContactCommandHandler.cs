using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Contacts.Commands.Update;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, UpdateContactCommandResult>
{
    private readonly IContactsRepository _contactsRepository;

    public UpdateContactCommandHandler(IContactsRepository contactsRepository)
    {
        _contactsRepository = contactsRepository;
    }

    public async Task<UpdateContactCommandResult> Handle(UpdateContactCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var contact = new Contact()
        {
            Id= request.Id,
            Name= request.Name,
            Surname= request.Surname
        };

        var result = await _contactsRepository.UpdateContact(contact);

        return new(result.Id, result.Name, result.Surname);
    }
}
