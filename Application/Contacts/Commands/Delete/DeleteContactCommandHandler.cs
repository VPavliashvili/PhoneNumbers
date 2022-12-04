using Domain.Abstractions;
using MediatR;

namespace Application.Contacts.Commands.Delete
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, bool>
    {
        private readonly IContactsRepository _contactsRepository;

        public DeleteContactCommandHandler(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        public async Task<bool> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            int id = request.Id;

            var res = await _contactsRepository.DeleteContact(id);
            return res;
        }
    }
}
