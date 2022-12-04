using Dapper;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ConnectionStrings _connectionStrings;

    public UsersRepository(IOptions<ConnectionStrings> options)
    {
        _connectionStrings = options.Value;
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByUsername(string userName)
    {
        var connection = new NpgsqlConnection(_connectionStrings.PhoneNumbers);

        string sql = "SELECT * FROM users u WHERE u.username = @usr";
        var args = new { usr = userName };

        try
        {
            var user = await connection.QuerySingleAsync<User>(sql, args);
            return user;
        }
        catch
        {
            throw;
        }
    }

    public async Task<User> RegisterUser(User user)
    {
        var connection = new NpgsqlConnection(_connectionStrings.PhoneNumbers);

        string sql = "INSERT INTO users (name, surname, username, password, salt) "
                   + "VALUES (@name, @snm, @usr, @pswd, @slt) RETURNING *;";

        var args = new { name = user.Name, snm = user.Surname, usr = user.Username, pswd = user.Password, slt = user.Salt };

        try
        {
            var inserted = await connection.QuerySingleAsync<User>(sql, args);
            return inserted;
        }
        catch
        {
            throw;
        }


    }
}
