namespace Application.Abstractions;

public interface IPasswordSecurityService
{
    string GetHashedPassword(string originalPassword, out string salt);
    bool IsMatchingWithHash(string enteredPassword, string storedHash, string storedSalt);
}
