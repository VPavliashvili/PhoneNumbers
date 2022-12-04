using Domain.Entities;

namespace Domain.Abstractions;

public interface IContactsRepository
{
    Task<Contact> AddContact(Contact contact);
    Task<Contact> UpdateContact(Contact contact);
    Task<bool> DeleteContact(int id);

    Task<ContactNumber> AddMobileNumberToContact(ContactNumber contactNumber);
    Task<bool> DeleteMobileNumberFromContact(int id, string mobileNumber);

    Task<Contact> GetContactByMobileNumber(string mobileNumber);
}
