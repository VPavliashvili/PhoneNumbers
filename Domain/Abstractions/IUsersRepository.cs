using Domain.Entities;

namespace Domain.Abstractions;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserByUsername(string userName);
    Task<User> RegisterUser(User user);
}