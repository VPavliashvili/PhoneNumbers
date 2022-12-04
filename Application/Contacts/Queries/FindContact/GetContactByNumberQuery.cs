using MediatR;

namespace Application.Contacts.Queries.FindContact;

public record GetContactByNumberQuery(string Number) : IRequest<GetContactByNumberQueryResponse>;
