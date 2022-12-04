using Dapper;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;
using Z.Dapper.Plus;

namespace Infrastructure.Repositories;

public class ContactsRepository : IContactsRepository
{

    private readonly ConnectionStrings _connectionStrings;

    public ContactsRepository(IOptions<ConnectionStrings> options)
    {
        _connectionStrings = options.Value;
    }

    public async Task<Contact> AddContact(Contact contact)
    {
        var connection = new NpgsqlConnection(_connectionStrings.PhoneNumbers);
        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            string sql = "INSERT INTO contacts (name, surname) VALUES (@name, @snm) RETURNING *;";

            var args = new { name = contact.Name, snm = contact.Surname };
            var inserted = await connection.QuerySingleAsync<Contact>(sql, args, transaction: transaction);

            int id = inserted.Id;

            foreach (var number in contact.PhoneNumbers)
            {
                sql = "INSERT INTO contact_numbers (contact_id, number) VALUES (@cid, @nmb);";
                await connection.ExecuteAsync(sql, new { cid = id, nmb = number }, transaction: transaction);
            }

            await transaction.CommitAsync();

            return inserted;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<bool> DeleteContact(int id)
    {
        var connection = new NpgsqlConnection(_connectionStrings.PhoneNumbers);
        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            
            string sql = "DELETE FROM contacts WHERE id = @id RETURNING *";
            await connection.QuerySingleAsync<Contact>(sql, new { id }, transaction: transaction);
            
            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("No contact found with such id");
        }
        finally
        {
            await connection.CloseAsync();
        }

    }

    public Task<string> AddMobileNumberToContact(string mobileNumber)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteMobileNumberFromContact(string mobileNumber)
    {
        throw new NotImplementedException();
    }

    public Task<Contact> GetContactByMobileNumber(string mobileNumber)
    {
        throw new NotImplementedException();
    }

    public Task<Contact> UpdateContact(Contact contact)
    {
        throw new NotImplementedException();
    }
}
