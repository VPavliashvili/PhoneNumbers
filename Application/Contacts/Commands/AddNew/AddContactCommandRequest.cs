//using Domain.Abstractions;
//using MediatR;

//namespace Application.Contacts.Commands.AddNew;

//public record AddContactCommandRequest(string Name, string Surname, string MobileNumber);

//public record AddContactCommand(AddContactCommandRequest Request) : IRequest<int>;

//public class AddContactCommandHandler : IRequestHandler<AddContactCommand, int>
//{
//    private readonly IContactsRepository _contactsRepository;

//    public AddContactCommandHandler(IContactsRepository contactsRepository)
//    {
//        _contactsRepository = contactsRepository;
//    }

//    public Task<int> Handle(AddContactCommand request, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }
//}