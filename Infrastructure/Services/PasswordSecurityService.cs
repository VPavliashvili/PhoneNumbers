using Application.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class PasswordSecurityService : IPasswordSecurityService
{
    public string GetHashedPassword(string originalPassword, out string salt)
    {
        salt = GetSalt();
        var result = GetHash(originalPassword, salt);

        return result;
    }

    public bool IsMatchingWithHash(string enteredPassword, string storedHash, string storedSalt)
    {
        var hashFromEnteredPassword = GetHash(enteredPassword, storedSalt);
        return storedHash == hashFromEnteredPassword;
    }

    private static string GetHash(string password, string salt)
    {
        using var md5 = MD5.Create();
        var combined = password + salt;
        var bytes = Encoding.UTF8.GetBytes(combined);
        var hashed = md5.ComputeHash(bytes);

        var result = Convert.ToHexString(hashed);
        return result;
    }

    private static string GetSalt()
    {
        int saltLength = 32;
        var salt = RandomNumberGenerator.GetBytes(saltLength);

        var result = Encoding.UTF8.GetString(salt);

        return result;
    }

}
