namespace Application.Users.Commands.RegisterUser;

public record RegisterUserCommandRequest(string Name, string Surname, string Username, string Password);
