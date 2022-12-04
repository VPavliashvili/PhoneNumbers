using Domain.Abstractions;
using MediatR;

namespace Application.Contacts.Queries.FindContact;

public class GetContactByNumberQueryHandler : IRequestHandler<GetContactByNumberQuery, GetContactByNumberQueryResponse>
{
    private readonly IContactsRepository _contactsRepository;

    public GetContactByNumberQueryHandler(IContactsRepository contactsRepository)
    {
        _contactsRepository = contactsRepository;   
    }

    public async Task<GetContactByNumberQueryResponse> Handle(GetContactByNumberQuery query, CancellationToken cancellationToken)
    {
        var number = query.Number;
        var contact = await _contactsRepository.GetContactByMobileNumber(number);

        var result = new GetContactByNumberQueryResponse(contact.Id, contact.Name, contact.Surname);
        return result;
    }
}
