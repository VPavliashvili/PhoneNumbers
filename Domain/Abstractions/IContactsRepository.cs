using Domain.Entities;

namespace Domain.Abstractions;

public interface IContactsRepository
{
    Task<Contact> AddContact(Contact contact);
    Task<Contact> UpdateContact(Contact contact);
    Task<bool> DeleteContact(int id);

    Task<string> AddMobileNumberToContact(string mobileNumber);
    Task<string> DeleteMobileNumberFromContact(string mobileNumber);

    Task<Contact> GetContactByMobileNumber(string mobileNumber);
}
