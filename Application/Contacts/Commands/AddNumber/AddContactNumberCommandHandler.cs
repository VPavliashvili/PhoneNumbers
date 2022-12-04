using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Contacts.Commands.AddNumber
{
    public class AddContactNumberCommandHandler : IRequestHandler<AddContactNumberCommand, int>
    {
        private readonly IContactsRepository _contactsRepository;

        public AddContactNumberCommandHandler(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;   
        }

        public async Task<int> Handle(AddContactNumberCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var contactNumber = new ContactNumber()
            {
                Contact_Id = request.Id,
                Number = request.Number
            };
            var res = await _contactsRepository.AddMobileNumberToContact(contactNumber);

            return res.Id;
        }
    }
}
