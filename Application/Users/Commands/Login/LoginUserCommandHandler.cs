using Application.Abstractions;
using Domain.Abstractions;
using MediatR;

namespace Application.Users.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IPasswordSecurityService _passwordSecurity;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHandler(IPasswordSecurityService passwordSecurity,
                                   IUsersRepository usersRepository,
                                   IJwtProvider jwtProvider)
    {
        _passwordSecurity = passwordSecurity;
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await _usersRepository.GetUserByUsername(request.Username);

        var storedHash = user.Password;
        var storedSalt = user.Salt;
        var inputPassword = request.Password;

        var isCorrectPassord = _passwordSecurity.IsMatchingWithHash(inputPassword, storedHash, storedSalt);

        if(!isCorrectPassord) 
        {
            throw new Exception("Username or password is incorrect");
        }

        string jwt = _jwtProvider.Generate(user);

        return jwt;
    }
}