namespace Application.Contacts.Commands.AddNew;

public record AddContactCommandRequest(string Name, string Surname, IEnumerable<string> MobileNumbers);
