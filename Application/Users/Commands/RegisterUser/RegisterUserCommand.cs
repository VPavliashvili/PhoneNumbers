using MediatR;

namespace Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(RegisterUserCommandRequest Request) : IRequest<int>;
