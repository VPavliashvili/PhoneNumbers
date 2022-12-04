using Dapper;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;

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
            await transaction.RollbackAsync();
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
            await transaction.RollbackAsync();
            throw new Exception("No contact found with such id");
        }
        finally
        {
            await connection.CloseAsync();
        }

    }

    public async Task<ContactNumber> AddMobileNumberToContact(ContactNumber contactNumber)
    {
        var connection = new NpgsqlConnection(_connectionStrings.PhoneNumbers);
        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            string sql = "INSERT INTO contact_numbers (contact_id, number) VALUES (@cid, @nmb) RETURNING *;";
            var args = new { cid = contactNumber.Contact_Id, nmb = contactNumber.Number };
            var newNumber = await connection.QuerySingleAsync<ContactNumber>(sql, args, transaction);

            await transaction.CommitAsync();

            return newNumber;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw new Exception("No contact found with such id");
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<bool> DeleteMobileNumberFromContact(int id, string mobileNumber)
    {
        var connection = new NpgsqlConnection(_connectionStrings.PhoneNumbers);
        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            string sql = "DELETE FROM contact_numbers WHERE contact_id = @id and number = @number RETURNING *;";
            var args = new { id, number = mobileNumber };
            await connection.QuerySingleAsync<ContactNumber>(sql, args, transaction: transaction);

            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw new Exception($"No record with Id {id} or not such number with given id");
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<Contact> UpdateContact(Contact contact)
    {
        var connection = new NpgsqlConnection(_connectionStrings.PhoneNumbers);
        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            string sql = "UPDATE contacts SET name = @nm, surname = @snm WHERE id = @id RETURNING *;";
            var args = new { nm = contact.Name, snm = contact.Surname, id = contact.Id };
            var result = await connection.QuerySingleAsync<Contact>(sql, args, transaction: transaction);

            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.CommitAsync();
            throw;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<Contact> GetContactByMobileNumber(string mobileNumber)
    {
        var connection = new NpgsqlConnection(_connectionStrings.PhoneNumbers);
        int contactId = default;

        try
        {
            string sql = "SELECT contact_id FROM contact_numbers WHERE number = @nmb";
            contactId = await connection.QuerySingleAsync<int>(sql, new { nmb = mobileNumber });

            sql = "SELECT * FROM contacts WHERE id = (" +
                     "SELECT contact_id FROM contact_numbers cn WHERE cn.contact_id = @cid GROUP BY cn.contact_id" +
                  ")";
            var contact = await connection.QuerySingleAsync<Contact>(sql, new { cid = contactId });

            return contact;
        }
        catch
        {
            throw new Exception($"Not such number for contactId {contactId}");
        }
    }
}
