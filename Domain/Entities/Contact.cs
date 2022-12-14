namespace Domain.Entities;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public IEnumerable<string> PhoneNumbers { get; set; }
}
