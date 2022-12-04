using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IPasswordSecurityService _passwordSecurity;
    private readonly IUsersRepository _usersRepository;

    public RegisterUserCommandHandler(
                    IPasswordSecurityService passwordSecurity,
                    IUsersRepository usersRepository)
    {
        _passwordSecurity = passwordSecurity;
        _usersRepository = usersRepository;
    }

    public async Task<int> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var hashedPassword = _passwordSecurity.GetHashedPassword(request.Password, out string salt);

        var user = new User()
        {
            Name = request.Name,
            Password = hashedPassword,
            Salt = salt,
            Surname = request.Surname,
            Username = request.Username,
        };

        try
        {
            var res = await _usersRepository.RegisterUser(user);
            return res.Id;
        }
        catch
        {
            throw;
        }
    }

}
