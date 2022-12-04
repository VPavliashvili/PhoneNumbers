using Application.Abstractions;
using Domain.Abstractions;
using MediatR;

namespace Application.Users.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IPasswordSecurityService _passwordSecurity;
    private readonly IUsersRepository _usersRepository;

    public LoginUserCommandHandler(IPasswordSecurityService passwordSecurity, IUsersRepository usersRepository)
    {
        _passwordSecurity = passwordSecurity;
        _usersRepository = usersRepository;
    }

    public async Task<string> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await _usersRepository.GetUserByUsername(request.Username);

        if (user == null)
        {
            throw new Exception($"User with name {request.Username} does not exist");
        }

        var storedHash = user.Password;
        var storedSalt = user.Salt;
        var inputPassword = request.Password;

        var isCorrectPassord = _passwordSecurity.IsMatchingWithHash(inputPassword, storedHash, storedSalt);

        if(!isCorrectPassord) 
        {
            throw new Exception("Username or password is incorrect");
        }

        throw new NotImplementedException("Need JWT implementation");
        //return "futureJWT";
    }
}